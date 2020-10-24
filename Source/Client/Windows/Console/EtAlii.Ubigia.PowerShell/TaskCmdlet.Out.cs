﻿namespace EtAlii.Ubigia.PowerShell
{
    using System;
    using System.Collections.Generic;
    using System.Management.Automation;
    using System.Threading.Tasks;

    public abstract class TaskCmdlet<TOut> : Cmdlet
        where TOut : class
    {
        /// <summary>
        /// Gets or sets the error collection
        /// </summary>
        protected List<ErrorRecord> Errors { get; private set; }

        /// <summary>
        /// Gets or sets the flag whether to write errors
        /// </summary>
        protected virtual bool WriteErrors { get; private set; }

        /// <summary>
        /// Performs an action />
        /// </summary>
        /// <returns>A <see cref="TOut"/></returns>
        protected abstract Task<TOut> ProcessTask();

        /// <summary>
        /// Initialises a new instance of the TaskCmdlet class
        /// </summary>
        protected TaskCmdlet()
        {
            Errors = new List<ErrorRecord>();
            WriteErrors = true;
        }

        /// <summary>
        /// Creates a collection of tasks to be processed
        /// </summary>
        /// <returns>A collection of tasks</returns>
        protected virtual IEnumerable<Task<TOut>> CreateProcessTasks()
        {
            yield return Task.Run(async () => await ProcessTask());
        }

        /// <summary>
        /// Performs the pipeline output for this cmdlet
        /// </summary>
        /// <param name="result"></param>
        protected virtual void PostProcessTask(TOut result)
        {
            WriteObject(result, true);
        }

        protected virtual Task BeginProcessingTask() => Task.CompletedTask;
        
        
        protected override void BeginProcessing()
        {
            try
            {
                var task = Task.Run(BeginProcessingTask);
                task.Wait();

                if (!WriteErrors) return;
                
                // Write errors
                foreach (var error in Errors)
                {
                    WriteError(error);
                }
            }
            catch (Exception e) when (e is PipelineStoppedException || e is PipelineClosedException)
            {
                // do nothing if pipeline stops
            }
            catch (AggregateException ae)
            {
                foreach (var e in ae.Flatten().InnerExceptions)
                {
                    WriteError(new ErrorRecord(e, e.GetType().Name, ErrorCategory.NotSpecified, this));
                }
            }
            catch (Exception e)
            {
                WriteError(new ErrorRecord(e, e.GetType().Name, ErrorCategory.NotSpecified, this));
            }
        }

        /// <summary>
        /// Processes cmdlet operation
        /// </summary>
        protected override void ProcessRecord()
        {
            try
            {
                var tasks = CreateProcessTasks();
                var results = Task.WhenAll(tasks);

                results.Wait();

                // Write results
                foreach (var result in results.Result)
                {
                    PostProcessTask(result);
                }

                if (!WriteErrors) return;
                
                // Write errors
                foreach (var error in Errors)
                {
                    WriteError(error);
                }
            }
            catch (Exception e) when (e is PipelineStoppedException || e is PipelineClosedException)
            {
                // do nothing if pipeline stops
            }
            catch (AggregateException ae)
            {
                foreach (var e in ae.Flatten().InnerExceptions)
                {
                    WriteError(new ErrorRecord(e, e.GetType().Name, ErrorCategory.NotSpecified, this));
                }
            }
            catch (Exception e)
            {
                WriteError(new ErrorRecord(e, e.GetType().Name, ErrorCategory.NotSpecified, this));
            }
        }
    }
}