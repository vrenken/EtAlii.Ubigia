// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Storage.proto
// </auto-generated>
// Original file comments:
//
// Copyright 2018 Peter Vrenken.
//
#pragma warning disable 1591
#region Designer generated code

using grpc = global::Grpc.Core;

namespace EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol {
  /// <summary>
  /// The Storage Grpc service definition.
  /// </summary>
  public static partial class StorageGrpcService
  {
    static readonly string __ServiceName = "EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageGrpcService";

    static readonly grpc::Marshaller<global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageRequest> __Marshaller_StorageRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageRequest.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageResponse> __Marshaller_StorageResponse = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageResponse.Parser.ParseFrom);

    static readonly grpc::Method<global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageRequest, global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageResponse> __Method_Get = new grpc::Method<global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageRequest, global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Get",
        __Marshaller_StorageRequest,
        __Marshaller_StorageResponse);

    static readonly grpc::Method<global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageRequest, global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageResponse> __Method_Post = new grpc::Method<global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageRequest, global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Post",
        __Marshaller_StorageRequest,
        __Marshaller_StorageResponse);

    static readonly grpc::Method<global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageRequest, global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageResponse> __Method_Put = new grpc::Method<global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageRequest, global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Put",
        __Marshaller_StorageRequest,
        __Marshaller_StorageResponse);

    static readonly grpc::Method<global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageRequest, global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageResponse> __Method_Delete = new grpc::Method<global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageRequest, global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Delete",
        __Marshaller_StorageRequest,
        __Marshaller_StorageResponse);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of StorageGrpcService</summary>
    public abstract partial class StorageGrpcServiceBase
    {
      public virtual global::System.Threading.Tasks.Task<global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageResponse> Get(global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task<global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageResponse> Post(global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task<global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageResponse> Put(global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task<global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageResponse> Delete(global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Client for StorageGrpcService</summary>
    public partial class StorageGrpcServiceClient : grpc::ClientBase<StorageGrpcServiceClient>
    {
      /// <summary>Creates a new client for StorageGrpcService</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      public StorageGrpcServiceClient(grpc::Channel channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for StorageGrpcService that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      public StorageGrpcServiceClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      protected StorageGrpcServiceClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      protected StorageGrpcServiceClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      public virtual global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageResponse Get(global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return Get(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageResponse Get(global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_Get, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageResponse> GetAsync(global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageResponse> GetAsync(global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_Get, null, options, request);
      }
      public virtual global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageResponse Post(global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return Post(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageResponse Post(global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_Post, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageResponse> PostAsync(global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return PostAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageResponse> PostAsync(global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_Post, null, options, request);
      }
      public virtual global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageResponse Put(global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return Put(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageResponse Put(global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_Put, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageResponse> PutAsync(global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return PutAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageResponse> PutAsync(global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_Put, null, options, request);
      }
      public virtual global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageResponse Delete(global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return Delete(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageResponse Delete(global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_Delete, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageResponse> DeleteAsync(global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return DeleteAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageResponse> DeleteAsync(global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.StorageRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_Delete, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      protected override StorageGrpcServiceClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new StorageGrpcServiceClient(configuration);
      }
    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static grpc::ServerServiceDefinition BindService(StorageGrpcServiceBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_Get, serviceImpl.Get)
          .AddMethod(__Method_Post, serviceImpl.Post)
          .AddMethod(__Method_Put, serviceImpl.Put)
          .AddMethod(__Method_Delete, serviceImpl.Delete).Build();
    }

  }
}
#endregion
