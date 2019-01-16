// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Content.proto
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
  /// The Content Grpc service definition.
  /// </summary>
  public static partial class ContentGrpcService
  {
    static readonly string __ServiceName = "EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentGrpcService";

    static readonly grpc::Marshaller<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentGetRequest> __Marshaller_EtAlii_Ubigia_Api_Transport_Grpc_WireProtocol_ContentGetRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentGetRequest.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentGetResponse> __Marshaller_EtAlii_Ubigia_Api_Transport_Grpc_WireProtocol_ContentGetResponse = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentGetResponse.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentPartGetRequest> __Marshaller_EtAlii_Ubigia_Api_Transport_Grpc_WireProtocol_ContentPartGetRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentPartGetRequest.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentPartGetResponse> __Marshaller_EtAlii_Ubigia_Api_Transport_Grpc_WireProtocol_ContentPartGetResponse = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentPartGetResponse.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentPostRequest> __Marshaller_EtAlii_Ubigia_Api_Transport_Grpc_WireProtocol_ContentPostRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentPostRequest.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentPostResponse> __Marshaller_EtAlii_Ubigia_Api_Transport_Grpc_WireProtocol_ContentPostResponse = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentPostResponse.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentPartPostRequest> __Marshaller_EtAlii_Ubigia_Api_Transport_Grpc_WireProtocol_ContentPartPostRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentPartPostRequest.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentPartPostResponse> __Marshaller_EtAlii_Ubigia_Api_Transport_Grpc_WireProtocol_ContentPartPostResponse = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentPartPostResponse.Parser.ParseFrom);

    static readonly grpc::Method<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentGetRequest, global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentGetResponse> __Method_Get = new grpc::Method<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentGetRequest, global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentGetResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Get",
        __Marshaller_EtAlii_Ubigia_Api_Transport_Grpc_WireProtocol_ContentGetRequest,
        __Marshaller_EtAlii_Ubigia_Api_Transport_Grpc_WireProtocol_ContentGetResponse);

    static readonly grpc::Method<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentPartGetRequest, global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentPartGetResponse> __Method_GetPart = new grpc::Method<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentPartGetRequest, global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentPartGetResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetPart",
        __Marshaller_EtAlii_Ubigia_Api_Transport_Grpc_WireProtocol_ContentPartGetRequest,
        __Marshaller_EtAlii_Ubigia_Api_Transport_Grpc_WireProtocol_ContentPartGetResponse);

    static readonly grpc::Method<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentPostRequest, global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentPostResponse> __Method_Post = new grpc::Method<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentPostRequest, global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentPostResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Post",
        __Marshaller_EtAlii_Ubigia_Api_Transport_Grpc_WireProtocol_ContentPostRequest,
        __Marshaller_EtAlii_Ubigia_Api_Transport_Grpc_WireProtocol_ContentPostResponse);

    static readonly grpc::Method<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentPartPostRequest, global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentPartPostResponse> __Method_PostPart = new grpc::Method<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentPartPostRequest, global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentPartPostResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "PostPart",
        __Marshaller_EtAlii_Ubigia_Api_Transport_Grpc_WireProtocol_ContentPartPostRequest,
        __Marshaller_EtAlii_Ubigia_Api_Transport_Grpc_WireProtocol_ContentPartPostResponse);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of ContentGrpcService</summary>
    public abstract partial class ContentGrpcServiceBase
    {
      public virtual global::System.Threading.Tasks.Task<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentGetResponse> Get(global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentGetRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentPartGetResponse> GetPart(global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentPartGetRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentPostResponse> Post(global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentPostRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentPartPostResponse> PostPart(global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentPartPostRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Client for ContentGrpcService</summary>
    public partial class ContentGrpcServiceClient : grpc::ClientBase<ContentGrpcServiceClient>
    {
      /// <summary>Creates a new client for ContentGrpcService</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      public ContentGrpcServiceClient(grpc::Channel channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for ContentGrpcService that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      public ContentGrpcServiceClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      protected ContentGrpcServiceClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      protected ContentGrpcServiceClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      public virtual global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentGetResponse Get(global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentGetRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return Get(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentGetResponse Get(global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentGetRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_Get, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentGetResponse> GetAsync(global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentGetRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentGetResponse> GetAsync(global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentGetRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_Get, null, options, request);
      }
      public virtual global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentPartGetResponse GetPart(global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentPartGetRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetPart(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentPartGetResponse GetPart(global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentPartGetRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_GetPart, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentPartGetResponse> GetPartAsync(global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentPartGetRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetPartAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentPartGetResponse> GetPartAsync(global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentPartGetRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_GetPart, null, options, request);
      }
      public virtual global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentPostResponse Post(global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentPostRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return Post(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentPostResponse Post(global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentPostRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_Post, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentPostResponse> PostAsync(global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentPostRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return PostAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentPostResponse> PostAsync(global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentPostRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_Post, null, options, request);
      }
      public virtual global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentPartPostResponse PostPart(global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentPartPostRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return PostPart(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentPartPostResponse PostPart(global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentPartPostRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_PostPart, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentPartPostResponse> PostPartAsync(global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentPartPostRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return PostPartAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentPartPostResponse> PostPartAsync(global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ContentPartPostRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_PostPart, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      protected override ContentGrpcServiceClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new ContentGrpcServiceClient(configuration);
      }
    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static grpc::ServerServiceDefinition BindService(ContentGrpcServiceBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_Get, serviceImpl.Get)
          .AddMethod(__Method_GetPart, serviceImpl.GetPart)
          .AddMethod(__Method_Post, serviceImpl.Post)
          .AddMethod(__Method_PostPart, serviceImpl.PostPart).Build();
    }

    /// <summary>Register service method implementations with a service binder. Useful when customizing the service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static void BindService(grpc::ServiceBinderBase serviceBinder, ContentGrpcServiceBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_Get, serviceImpl.Get);
      serviceBinder.AddMethod(__Method_GetPart, serviceImpl.GetPart);
      serviceBinder.AddMethod(__Method_Post, serviceImpl.Post);
      serviceBinder.AddMethod(__Method_PostPart, serviceImpl.PostPart);
    }

  }
}
#endregion
