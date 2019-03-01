// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Authentication.proto
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
  /// The Authentication Grpc service definition.
  /// </summary>
  public static partial class AuthenticationGrpcService
  {
    static readonly string __ServiceName = "EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.AuthenticationGrpcService";

    static readonly grpc::Marshaller<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.AuthenticationRequest> __Marshaller_EtAlii_Ubigia_Api_Transport_Grpc_WireProtocol_AuthenticationRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.AuthenticationRequest.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.AuthenticationResponse> __Marshaller_EtAlii_Ubigia_Api_Transport_Grpc_WireProtocol_AuthenticationResponse = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.AuthenticationResponse.Parser.ParseFrom);

    static readonly grpc::Method<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.AuthenticationRequest, global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.AuthenticationResponse> __Method_Authenticate = new grpc::Method<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.AuthenticationRequest, global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.AuthenticationResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Authenticate",
        __Marshaller_EtAlii_Ubigia_Api_Transport_Grpc_WireProtocol_AuthenticationRequest,
        __Marshaller_EtAlii_Ubigia_Api_Transport_Grpc_WireProtocol_AuthenticationResponse);

    static readonly grpc::Method<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.AuthenticationRequest, global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.AuthenticationResponse> __Method_AuthenticateAs = new grpc::Method<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.AuthenticationRequest, global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.AuthenticationResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "AuthenticateAs",
        __Marshaller_EtAlii_Ubigia_Api_Transport_Grpc_WireProtocol_AuthenticationRequest,
        __Marshaller_EtAlii_Ubigia_Api_Transport_Grpc_WireProtocol_AuthenticationResponse);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.AuthenticationReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of AuthenticationGrpcService</summary>
    public abstract partial class AuthenticationGrpcServiceBase
    {
      /// <summary>
      /// Try to authenticate.
      /// </summary>
      /// <param name="request">The request received from the client.</param>
      /// <param name="context">The context of the server-side call handler being invoked.</param>
      /// <returns>The response to send back to the client (wrapped by a task).</returns>
      public virtual global::System.Threading.Tasks.Task<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.AuthenticationResponse> Authenticate(global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.AuthenticationRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      /// <summary>
      /// Try to authenticate as another user.
      /// </summary>
      /// <param name="request">The request received from the client.</param>
      /// <param name="context">The context of the server-side call handler being invoked.</param>
      /// <returns>The response to send back to the client (wrapped by a task).</returns>
      public virtual global::System.Threading.Tasks.Task<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.AuthenticationResponse> AuthenticateAs(global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.AuthenticationRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Client for AuthenticationGrpcService</summary>
    public partial class AuthenticationGrpcServiceClient : grpc::ClientBase<AuthenticationGrpcServiceClient>
    {
      /// <summary>Creates a new client for AuthenticationGrpcService</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      public AuthenticationGrpcServiceClient(grpc::Channel channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for AuthenticationGrpcService that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      public AuthenticationGrpcServiceClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      protected AuthenticationGrpcServiceClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      protected AuthenticationGrpcServiceClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      /// <summary>
      /// Try to authenticate.
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.AuthenticationResponse Authenticate(global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.AuthenticationRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return Authenticate(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// Try to authenticate.
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.AuthenticationResponse Authenticate(global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.AuthenticationRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_Authenticate, null, options, request);
      }
      /// <summary>
      /// Try to authenticate.
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.AuthenticationResponse> AuthenticateAsync(global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.AuthenticationRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return AuthenticateAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// Try to authenticate.
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.AuthenticationResponse> AuthenticateAsync(global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.AuthenticationRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_Authenticate, null, options, request);
      }
      /// <summary>
      /// Try to authenticate as another user.
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.AuthenticationResponse AuthenticateAs(global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.AuthenticationRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return AuthenticateAs(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// Try to authenticate as another user.
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.AuthenticationResponse AuthenticateAs(global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.AuthenticationRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_AuthenticateAs, null, options, request);
      }
      /// <summary>
      /// Try to authenticate as another user.
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.AuthenticationResponse> AuthenticateAsAsync(global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.AuthenticationRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return AuthenticateAsAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// Try to authenticate as another user.
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.AuthenticationResponse> AuthenticateAsAsync(global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.AuthenticationRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_AuthenticateAs, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      protected override AuthenticationGrpcServiceClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new AuthenticationGrpcServiceClient(configuration);
      }
    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static grpc::ServerServiceDefinition BindService(AuthenticationGrpcServiceBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_Authenticate, serviceImpl.Authenticate)
          .AddMethod(__Method_AuthenticateAs, serviceImpl.AuthenticateAs).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the  service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static void BindService(grpc::ServiceBinderBase serviceBinder, AuthenticationGrpcServiceBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_Authenticate, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.AuthenticationRequest, global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.AuthenticationResponse>(serviceImpl.Authenticate));
      serviceBinder.AddMethod(__Method_AuthenticateAs, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.AuthenticationRequest, global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.AuthenticationResponse>(serviceImpl.AuthenticateAs));
    }

  }
}
#endregion
