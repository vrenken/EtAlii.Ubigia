// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Root.proto
// </auto-generated>
// Original file comments:
//
// Copyright 2018 Peter Vrenken.
//
#pragma warning disable 0414, 1591
#region Designer generated code

using grpc = global::Grpc.Core;

namespace EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol {
  /// <summary>
  /// The Root Grpc service definition.
  /// </summary>
  public static partial class RootGrpcService
  {
    static readonly string __ServiceName = "EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootGrpcService";

    static readonly grpc::Marshaller<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootSingleRequest> __Marshaller_EtAlii_Ubigia_Api_Transport_Grpc_WireProtocol_RootSingleRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootSingleRequest.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootSingleResponse> __Marshaller_EtAlii_Ubigia_Api_Transport_Grpc_WireProtocol_RootSingleResponse = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootSingleResponse.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootMultipleRequest> __Marshaller_EtAlii_Ubigia_Api_Transport_Grpc_WireProtocol_RootMultipleRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootMultipleRequest.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootMultipleResponse> __Marshaller_EtAlii_Ubigia_Api_Transport_Grpc_WireProtocol_RootMultipleResponse = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootMultipleResponse.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootPostSingleRequest> __Marshaller_EtAlii_Ubigia_Api_Transport_Grpc_WireProtocol_RootPostSingleRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootPostSingleRequest.Parser.ParseFrom);

    static readonly grpc::Method<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootSingleRequest, global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootSingleResponse> __Method_GetSingle = new grpc::Method<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootSingleRequest, global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootSingleResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetSingle",
        __Marshaller_EtAlii_Ubigia_Api_Transport_Grpc_WireProtocol_RootSingleRequest,
        __Marshaller_EtAlii_Ubigia_Api_Transport_Grpc_WireProtocol_RootSingleResponse);

    static readonly grpc::Method<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootMultipleRequest, global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootMultipleResponse> __Method_GetMultiple = new grpc::Method<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootMultipleRequest, global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootMultipleResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetMultiple",
        __Marshaller_EtAlii_Ubigia_Api_Transport_Grpc_WireProtocol_RootMultipleRequest,
        __Marshaller_EtAlii_Ubigia_Api_Transport_Grpc_WireProtocol_RootMultipleResponse);

    static readonly grpc::Method<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootPostSingleRequest, global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootSingleResponse> __Method_Post = new grpc::Method<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootPostSingleRequest, global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootSingleResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Post",
        __Marshaller_EtAlii_Ubigia_Api_Transport_Grpc_WireProtocol_RootPostSingleRequest,
        __Marshaller_EtAlii_Ubigia_Api_Transport_Grpc_WireProtocol_RootSingleResponse);

    static readonly grpc::Method<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootSingleRequest, global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootSingleResponse> __Method_Put = new grpc::Method<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootSingleRequest, global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootSingleResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Put",
        __Marshaller_EtAlii_Ubigia_Api_Transport_Grpc_WireProtocol_RootSingleRequest,
        __Marshaller_EtAlii_Ubigia_Api_Transport_Grpc_WireProtocol_RootSingleResponse);

    static readonly grpc::Method<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootSingleRequest, global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootSingleResponse> __Method_Delete = new grpc::Method<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootSingleRequest, global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootSingleResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Delete",
        __Marshaller_EtAlii_Ubigia_Api_Transport_Grpc_WireProtocol_RootSingleRequest,
        __Marshaller_EtAlii_Ubigia_Api_Transport_Grpc_WireProtocol_RootSingleResponse);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of RootGrpcService</summary>
    public abstract partial class RootGrpcServiceBase
    {
      public virtual global::System.Threading.Tasks.Task<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootSingleResponse> GetSingle(global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootSingleRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootMultipleResponse> GetMultiple(global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootMultipleRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootSingleResponse> Post(global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootPostSingleRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootSingleResponse> Put(global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootSingleRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootSingleResponse> Delete(global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootSingleRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Client for RootGrpcService</summary>
    public partial class RootGrpcServiceClient : grpc::ClientBase<RootGrpcServiceClient>
    {
      /// <summary>Creates a new client for RootGrpcService</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      public RootGrpcServiceClient(grpc::Channel channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for RootGrpcService that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      public RootGrpcServiceClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      protected RootGrpcServiceClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      protected RootGrpcServiceClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      public virtual global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootSingleResponse GetSingle(global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootSingleRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetSingle(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootSingleResponse GetSingle(global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootSingleRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_GetSingle, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootSingleResponse> GetSingleAsync(global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootSingleRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetSingleAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootSingleResponse> GetSingleAsync(global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootSingleRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_GetSingle, null, options, request);
      }
      public virtual global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootMultipleResponse GetMultiple(global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootMultipleRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetMultiple(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootMultipleResponse GetMultiple(global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootMultipleRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_GetMultiple, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootMultipleResponse> GetMultipleAsync(global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootMultipleRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetMultipleAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootMultipleResponse> GetMultipleAsync(global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootMultipleRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_GetMultiple, null, options, request);
      }
      public virtual global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootSingleResponse Post(global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootPostSingleRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return Post(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootSingleResponse Post(global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootPostSingleRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_Post, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootSingleResponse> PostAsync(global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootPostSingleRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return PostAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootSingleResponse> PostAsync(global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootPostSingleRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_Post, null, options, request);
      }
      public virtual global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootSingleResponse Put(global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootSingleRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return Put(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootSingleResponse Put(global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootSingleRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_Put, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootSingleResponse> PutAsync(global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootSingleRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return PutAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootSingleResponse> PutAsync(global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootSingleRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_Put, null, options, request);
      }
      public virtual global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootSingleResponse Delete(global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootSingleRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return Delete(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootSingleResponse Delete(global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootSingleRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_Delete, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootSingleResponse> DeleteAsync(global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootSingleRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return DeleteAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootSingleResponse> DeleteAsync(global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootSingleRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_Delete, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      protected override RootGrpcServiceClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new RootGrpcServiceClient(configuration);
      }
    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static grpc::ServerServiceDefinition BindService(RootGrpcServiceBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_GetSingle, serviceImpl.GetSingle)
          .AddMethod(__Method_GetMultiple, serviceImpl.GetMultiple)
          .AddMethod(__Method_Post, serviceImpl.Post)
          .AddMethod(__Method_Put, serviceImpl.Put)
          .AddMethod(__Method_Delete, serviceImpl.Delete).Build();
    }

    /// <summary>Register service method implementations with a service binder. Useful when customizing the service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static void BindService(grpc::ServiceBinderBase serviceBinder, RootGrpcServiceBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_GetSingle, serviceImpl.GetSingle);
      serviceBinder.AddMethod(__Method_GetMultiple, serviceImpl.GetMultiple);
      serviceBinder.AddMethod(__Method_Post, serviceImpl.Post);
      serviceBinder.AddMethod(__Method_Put, serviceImpl.Put);
      serviceBinder.AddMethod(__Method_Delete, serviceImpl.Delete);
    }

  }
}
#endregion
