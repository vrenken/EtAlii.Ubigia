// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: Root.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol {

  /// <summary>Holder for reflection information generated from Root.proto</summary>
  public static partial class RootReflection {

    #region Descriptor
    /// <summary>File descriptor for Root.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static RootReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "CgpSb290LnByb3RvEi1FdEFsaWkuVWJpZ2lhLkFwaS5UcmFuc3BvcnQuR3Jw",
            "Yy5XaXJlUHJvdG9jb2waDF9Nb2RlbC5wcm90byL+AQoRUm9vdFNpbmdsZVJl",
            "cXVlc3QSRAoHU3BhY2VJZBgBIAEoCzIzLkV0QWxpaS5VYmlnaWEuQXBpLlRy",
            "YW5zcG9ydC5HcnBjLldpcmVQcm90b2NvbC5HdWlkEkEKAklkGAIgASgLMjMu",
            "RXRBbGlpLlViaWdpYS5BcGkuVHJhbnNwb3J0LkdycGMuV2lyZVByb3RvY29s",
            "Lkd1aWRIABIOCgROYW1lGAMgASgJSAASQwoEUm9vdBgEIAEoCzIzLkV0QWxp",
            "aS5VYmlnaWEuQXBpLlRyYW5zcG9ydC5HcnBjLldpcmVQcm90b2NvbC5Sb290",
            "SABCCwoJU2VsZWN0aW9uIqABChVSb290UG9zdFNpbmdsZVJlcXVlc3QSRAoH",
            "U3BhY2VJZBgBIAEoCzIzLkV0QWxpaS5VYmlnaWEuQXBpLlRyYW5zcG9ydC5H",
            "cnBjLldpcmVQcm90b2NvbC5HdWlkEkEKBFJvb3QYAiABKAsyMy5FdEFsaWku",
            "VWJpZ2lhLkFwaS5UcmFuc3BvcnQuR3JwYy5XaXJlUHJvdG9jb2wuUm9vdCIV",
            "ChNSb290TXVsdGlwbGVSZXF1ZXN0IlcKElJvb3RTaW5nbGVSZXNwb25zZRJB",
            "CgRSb290GAEgASgLMjMuRXRBbGlpLlViaWdpYS5BcGkuVHJhbnNwb3J0Lkdy",
            "cGMuV2lyZVByb3RvY29sLlJvb3QiWgoUUm9vdE11bHRpcGxlUmVzcG9uc2US",
            "QgoFUm9vdHMYASADKAsyMy5FdEFsaWkuVWJpZ2lhLkFwaS5UcmFuc3BvcnQu",
            "R3JwYy5XaXJlUHJvdG9jb2wuUm9vdDL2BQoPUm9vdEdycGNTZXJ2aWNlEpIB",
            "CglHZXRTaW5nbGUSQC5FdEFsaWkuVWJpZ2lhLkFwaS5UcmFuc3BvcnQuR3Jw",
            "Yy5XaXJlUHJvdG9jb2wuUm9vdFNpbmdsZVJlcXVlc3QaQS5FdEFsaWkuVWJp",
            "Z2lhLkFwaS5UcmFuc3BvcnQuR3JwYy5XaXJlUHJvdG9jb2wuUm9vdFNpbmds",
            "ZVJlc3BvbnNlIgASmAEKC0dldE11bHRpcGxlEkIuRXRBbGlpLlViaWdpYS5B",
            "cGkuVHJhbnNwb3J0LkdycGMuV2lyZVByb3RvY29sLlJvb3RNdWx0aXBsZVJl",
            "cXVlc3QaQy5FdEFsaWkuVWJpZ2lhLkFwaS5UcmFuc3BvcnQuR3JwYy5XaXJl",
            "UHJvdG9jb2wuUm9vdE11bHRpcGxlUmVzcG9uc2UiABKRAQoEUG9zdBJELkV0",
            "QWxpaS5VYmlnaWEuQXBpLlRyYW5zcG9ydC5HcnBjLldpcmVQcm90b2NvbC5S",
            "b290UG9zdFNpbmdsZVJlcXVlc3QaQS5FdEFsaWkuVWJpZ2lhLkFwaS5UcmFu",
            "c3BvcnQuR3JwYy5XaXJlUHJvdG9jb2wuUm9vdFNpbmdsZVJlc3BvbnNlIgAS",
            "jAEKA1B1dBJALkV0QWxpaS5VYmlnaWEuQXBpLlRyYW5zcG9ydC5HcnBjLldp",
            "cmVQcm90b2NvbC5Sb290U2luZ2xlUmVxdWVzdBpBLkV0QWxpaS5VYmlnaWEu",
            "QXBpLlRyYW5zcG9ydC5HcnBjLldpcmVQcm90b2NvbC5Sb290U2luZ2xlUmVz",
            "cG9uc2UiABKPAQoGRGVsZXRlEkAuRXRBbGlpLlViaWdpYS5BcGkuVHJhbnNw",
            "b3J0LkdycGMuV2lyZVByb3RvY29sLlJvb3RTaW5nbGVSZXF1ZXN0GkEuRXRB",
            "bGlpLlViaWdpYS5BcGkuVHJhbnNwb3J0LkdycGMuV2lyZVByb3RvY29sLlJv",
            "b3RTaW5nbGVSZXNwb25zZSIAQnIKLUV0QWxpaS5VYmlnaWEuQXBpLlRyYW5z",
            "cG9ydC5HcnBjLldpcmVQcm90b2NvbEIGVWJpZ2lhUAGiAgZVYmlnaWGqAi1F",
            "dEFsaWkuVWJpZ2lhLkFwaS5UcmFuc3BvcnQuR3JwYy5XaXJlUHJvdG9jb2xi",
            "BnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ModelReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootSingleRequest), global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootSingleRequest.Parser, new[]{ "SpaceId", "Id", "Name", "Root" }, new[]{ "Selection" }, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootPostSingleRequest), global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootPostSingleRequest.Parser, new[]{ "SpaceId", "Root" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootMultipleRequest), global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootMultipleRequest.Parser, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootSingleResponse), global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootSingleResponse.Parser, new[]{ "Root" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootMultipleResponse), global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootMultipleResponse.Parser, new[]{ "Roots" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class RootSingleRequest : pb::IMessage<RootSingleRequest> {
    private static readonly pb::MessageParser<RootSingleRequest> _parser = new pb::MessageParser<RootSingleRequest>(() => new RootSingleRequest());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<RootSingleRequest> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public RootSingleRequest() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public RootSingleRequest(RootSingleRequest other) : this() {
      SpaceId = other.spaceId_ != null ? other.SpaceId.Clone() : null;
      switch (other.SelectionCase) {
        case SelectionOneofCase.Id:
          Id = other.Id.Clone();
          break;
        case SelectionOneofCase.Name:
          Name = other.Name;
          break;
        case SelectionOneofCase.Root:
          Root = other.Root.Clone();
          break;
      }

    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public RootSingleRequest Clone() {
      return new RootSingleRequest(this);
    }

    /// <summary>Field number for the "SpaceId" field.</summary>
    public const int SpaceIdFieldNumber = 1;
    private global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.Guid spaceId_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.Guid SpaceId {
      get { return spaceId_; }
      set {
        spaceId_ = value;
      }
    }

    /// <summary>Field number for the "Id" field.</summary>
    public const int IdFieldNumber = 2;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.Guid Id {
      get { return selectionCase_ == SelectionOneofCase.Id ? (global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.Guid) selection_ : null; }
      set {
        selection_ = value;
        selectionCase_ = value == null ? SelectionOneofCase.None : SelectionOneofCase.Id;
      }
    }

    /// <summary>Field number for the "Name" field.</summary>
    public const int NameFieldNumber = 3;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Name {
      get { return selectionCase_ == SelectionOneofCase.Name ? (string) selection_ : ""; }
      set {
        selection_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
        selectionCase_ = SelectionOneofCase.Name;
      }
    }

    /// <summary>Field number for the "Root" field.</summary>
    public const int RootFieldNumber = 4;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.Root Root {
      get { return selectionCase_ == SelectionOneofCase.Root ? (global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.Root) selection_ : null; }
      set {
        selection_ = value;
        selectionCase_ = value == null ? SelectionOneofCase.None : SelectionOneofCase.Root;
      }
    }

    private object selection_;
    /// <summary>Enum of possible cases for the "Selection" oneof.</summary>
    public enum SelectionOneofCase {
      None = 0,
      Id = 2,
      Name = 3,
      Root = 4,
    }
    private SelectionOneofCase selectionCase_ = SelectionOneofCase.None;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public SelectionOneofCase SelectionCase {
      get { return selectionCase_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void ClearSelection() {
      selectionCase_ = SelectionOneofCase.None;
      selection_ = null;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as RootSingleRequest);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(RootSingleRequest other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!object.Equals(SpaceId, other.SpaceId)) return false;
      if (!object.Equals(Id, other.Id)) return false;
      if (Name != other.Name) return false;
      if (!object.Equals(Root, other.Root)) return false;
      if (SelectionCase != other.SelectionCase) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (spaceId_ != null) hash ^= SpaceId.GetHashCode();
      if (selectionCase_ == SelectionOneofCase.Id) hash ^= Id.GetHashCode();
      if (selectionCase_ == SelectionOneofCase.Name) hash ^= Name.GetHashCode();
      if (selectionCase_ == SelectionOneofCase.Root) hash ^= Root.GetHashCode();
      hash ^= (int) selectionCase_;
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (spaceId_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(SpaceId);
      }
      if (selectionCase_ == SelectionOneofCase.Id) {
        output.WriteRawTag(18);
        output.WriteMessage(Id);
      }
      if (selectionCase_ == SelectionOneofCase.Name) {
        output.WriteRawTag(26);
        output.WriteString(Name);
      }
      if (selectionCase_ == SelectionOneofCase.Root) {
        output.WriteRawTag(34);
        output.WriteMessage(Root);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (spaceId_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(SpaceId);
      }
      if (selectionCase_ == SelectionOneofCase.Id) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Id);
      }
      if (selectionCase_ == SelectionOneofCase.Name) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Name);
      }
      if (selectionCase_ == SelectionOneofCase.Root) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Root);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(RootSingleRequest other) {
      if (other == null) {
        return;
      }
      if (other.spaceId_ != null) {
        if (spaceId_ == null) {
          spaceId_ = new global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.Guid();
        }
        SpaceId.MergeFrom(other.SpaceId);
      }
      switch (other.SelectionCase) {
        case SelectionOneofCase.Id:
          if (Id == null) {
            Id = new global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.Guid();
          }
          Id.MergeFrom(other.Id);
          break;
        case SelectionOneofCase.Name:
          Name = other.Name;
          break;
        case SelectionOneofCase.Root:
          if (Root == null) {
            Root = new global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.Root();
          }
          Root.MergeFrom(other.Root);
          break;
      }

    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 10: {
            if (spaceId_ == null) {
              spaceId_ = new global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.Guid();
            }
            input.ReadMessage(spaceId_);
            break;
          }
          case 18: {
            global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.Guid subBuilder = new global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.Guid();
            if (selectionCase_ == SelectionOneofCase.Id) {
              subBuilder.MergeFrom(Id);
            }
            input.ReadMessage(subBuilder);
            Id = subBuilder;
            break;
          }
          case 26: {
            Name = input.ReadString();
            break;
          }
          case 34: {
            global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.Root subBuilder = new global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.Root();
            if (selectionCase_ == SelectionOneofCase.Root) {
              subBuilder.MergeFrom(Root);
            }
            input.ReadMessage(subBuilder);
            Root = subBuilder;
            break;
          }
        }
      }
    }

  }

  public sealed partial class RootPostSingleRequest : pb::IMessage<RootPostSingleRequest> {
    private static readonly pb::MessageParser<RootPostSingleRequest> _parser = new pb::MessageParser<RootPostSingleRequest>(() => new RootPostSingleRequest());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<RootPostSingleRequest> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public RootPostSingleRequest() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public RootPostSingleRequest(RootPostSingleRequest other) : this() {
      SpaceId = other.spaceId_ != null ? other.SpaceId.Clone() : null;
      Root = other.root_ != null ? other.Root.Clone() : null;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public RootPostSingleRequest Clone() {
      return new RootPostSingleRequest(this);
    }

    /// <summary>Field number for the "SpaceId" field.</summary>
    public const int SpaceIdFieldNumber = 1;
    private global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.Guid spaceId_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.Guid SpaceId {
      get { return spaceId_; }
      set {
        spaceId_ = value;
      }
    }

    /// <summary>Field number for the "Root" field.</summary>
    public const int RootFieldNumber = 2;
    private global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.Root root_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.Root Root {
      get { return root_; }
      set {
        root_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as RootPostSingleRequest);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(RootPostSingleRequest other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!object.Equals(SpaceId, other.SpaceId)) return false;
      if (!object.Equals(Root, other.Root)) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (spaceId_ != null) hash ^= SpaceId.GetHashCode();
      if (root_ != null) hash ^= Root.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (spaceId_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(SpaceId);
      }
      if (root_ != null) {
        output.WriteRawTag(18);
        output.WriteMessage(Root);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (spaceId_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(SpaceId);
      }
      if (root_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Root);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(RootPostSingleRequest other) {
      if (other == null) {
        return;
      }
      if (other.spaceId_ != null) {
        if (spaceId_ == null) {
          spaceId_ = new global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.Guid();
        }
        SpaceId.MergeFrom(other.SpaceId);
      }
      if (other.root_ != null) {
        if (root_ == null) {
          root_ = new global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.Root();
        }
        Root.MergeFrom(other.Root);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 10: {
            if (spaceId_ == null) {
              spaceId_ = new global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.Guid();
            }
            input.ReadMessage(spaceId_);
            break;
          }
          case 18: {
            if (root_ == null) {
              root_ = new global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.Root();
            }
            input.ReadMessage(root_);
            break;
          }
        }
      }
    }

  }

  public sealed partial class RootMultipleRequest : pb::IMessage<RootMultipleRequest> {
    private static readonly pb::MessageParser<RootMultipleRequest> _parser = new pb::MessageParser<RootMultipleRequest>(() => new RootMultipleRequest());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<RootMultipleRequest> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootReflection.Descriptor.MessageTypes[2]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public RootMultipleRequest() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public RootMultipleRequest(RootMultipleRequest other) : this() {
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public RootMultipleRequest Clone() {
      return new RootMultipleRequest(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as RootMultipleRequest);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(RootMultipleRequest other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(RootMultipleRequest other) {
      if (other == null) {
        return;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
        }
      }
    }

  }

  public sealed partial class RootSingleResponse : pb::IMessage<RootSingleResponse> {
    private static readonly pb::MessageParser<RootSingleResponse> _parser = new pb::MessageParser<RootSingleResponse>(() => new RootSingleResponse());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<RootSingleResponse> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootReflection.Descriptor.MessageTypes[3]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public RootSingleResponse() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public RootSingleResponse(RootSingleResponse other) : this() {
      Root = other.root_ != null ? other.Root.Clone() : null;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public RootSingleResponse Clone() {
      return new RootSingleResponse(this);
    }

    /// <summary>Field number for the "Root" field.</summary>
    public const int RootFieldNumber = 1;
    private global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.Root root_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.Root Root {
      get { return root_; }
      set {
        root_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as RootSingleResponse);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(RootSingleResponse other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!object.Equals(Root, other.Root)) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (root_ != null) hash ^= Root.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (root_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(Root);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (root_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Root);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(RootSingleResponse other) {
      if (other == null) {
        return;
      }
      if (other.root_ != null) {
        if (root_ == null) {
          root_ = new global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.Root();
        }
        Root.MergeFrom(other.Root);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 10: {
            if (root_ == null) {
              root_ = new global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.Root();
            }
            input.ReadMessage(root_);
            break;
          }
        }
      }
    }

  }

  public sealed partial class RootMultipleResponse : pb::IMessage<RootMultipleResponse> {
    private static readonly pb::MessageParser<RootMultipleResponse> _parser = new pb::MessageParser<RootMultipleResponse>(() => new RootMultipleResponse());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<RootMultipleResponse> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.RootReflection.Descriptor.MessageTypes[4]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public RootMultipleResponse() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public RootMultipleResponse(RootMultipleResponse other) : this() {
      roots_ = other.roots_.Clone();
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public RootMultipleResponse Clone() {
      return new RootMultipleResponse(this);
    }

    /// <summary>Field number for the "Roots" field.</summary>
    public const int RootsFieldNumber = 1;
    private static readonly pb::FieldCodec<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.Root> _repeated_roots_codec
        = pb::FieldCodec.ForMessage(10, global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.Root.Parser);
    private readonly pbc::RepeatedField<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.Root> roots_ = new pbc::RepeatedField<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.Root>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.Root> Roots {
      get { return roots_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as RootMultipleResponse);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(RootMultipleResponse other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if(!roots_.Equals(other.roots_)) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      hash ^= roots_.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      roots_.WriteTo(output, _repeated_roots_codec);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      size += roots_.CalculateSize(_repeated_roots_codec);
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(RootMultipleResponse other) {
      if (other == null) {
        return;
      }
      roots_.Add(other.roots_);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 10: {
            roots_.AddEntriesFrom(input, _repeated_roots_codec);
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
