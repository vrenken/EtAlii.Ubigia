// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Properties.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol {

  /// <summary>Holder for reflection information generated from Properties.proto</summary>
  public static partial class PropertiesReflection {

    #region Descriptor
    /// <summary>File descriptor for Properties.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static PropertiesReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChBQcm9wZXJ0aWVzLnByb3RvEi1FdEFsaWkuVWJpZ2lhLkFwaS5UcmFuc3Bv",
            "cnQuR3JwYy5XaXJlUHJvdG9jb2waDF9Nb2RlbC5wcm90byJiChRQcm9wZXJ0",
            "aWVzR2V0UmVxdWVzdBJKCgdFbnRyeUlkGAEgASgLMjkuRXRBbGlpLlViaWdp",
            "YS5BcGkuVHJhbnNwb3J0LkdycGMuV2lyZVByb3RvY29sLklkZW50aWZpZXIi",
            "dgoVUHJvcGVydGllc0dldFJlc3BvbnNlEl0KElByb3BlcnR5RGljdGlvbmFy",
            "eRgBIAEoCzJBLkV0QWxpaS5VYmlnaWEuQXBpLlRyYW5zcG9ydC5HcnBjLldp",
            "cmVQcm90b2NvbC5Qcm9wZXJ0eURpY3Rpb25hcnkiwgEKFVByb3BlcnRpZXNQ",
            "b3N0UmVxdWVzdBJKCgdFbnRyeUlkGAEgASgLMjkuRXRBbGlpLlViaWdpYS5B",
            "cGkuVHJhbnNwb3J0LkdycGMuV2lyZVByb3RvY29sLklkZW50aWZpZXISXQoS",
            "UHJvcGVydHlEaWN0aW9uYXJ5GAIgASgLMkEuRXRBbGlpLlViaWdpYS5BcGku",
            "VHJhbnNwb3J0LkdycGMuV2lyZVByb3RvY29sLlByb3BlcnR5RGljdGlvbmFy",
            "eSIYChZQcm9wZXJ0aWVzUG9zdFJlc3BvbnNlMsQCChVQcm9wZXJ0aWVzR3Jw",
            "Y1NlcnZpY2USkgEKA0dldBJDLkV0QWxpaS5VYmlnaWEuQXBpLlRyYW5zcG9y",
            "dC5HcnBjLldpcmVQcm90b2NvbC5Qcm9wZXJ0aWVzR2V0UmVxdWVzdBpELkV0",
            "QWxpaS5VYmlnaWEuQXBpLlRyYW5zcG9ydC5HcnBjLldpcmVQcm90b2NvbC5Q",
            "cm9wZXJ0aWVzR2V0UmVzcG9uc2UiABKVAQoEUG9zdBJELkV0QWxpaS5VYmln",
            "aWEuQXBpLlRyYW5zcG9ydC5HcnBjLldpcmVQcm90b2NvbC5Qcm9wZXJ0aWVz",
            "UG9zdFJlcXVlc3QaRS5FdEFsaWkuVWJpZ2lhLkFwaS5UcmFuc3BvcnQuR3Jw",
            "Yy5XaXJlUHJvdG9jb2wuUHJvcGVydGllc1Bvc3RSZXNwb25zZSIAQnIKLUV0",
            "QWxpaS5VYmlnaWEuQXBpLlRyYW5zcG9ydC5HcnBjLldpcmVQcm90b2NvbEIG",
            "VWJpZ2lhUAGiAgZVYmlnaWGqAi1FdEFsaWkuVWJpZ2lhLkFwaS5UcmFuc3Bv",
            "cnQuR3JwYy5XaXJlUHJvdG9jb2xiBnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ModelReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.PropertiesGetRequest), global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.PropertiesGetRequest.Parser, new[]{ "EntryId" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.PropertiesGetResponse), global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.PropertiesGetResponse.Parser, new[]{ "PropertyDictionary" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.PropertiesPostRequest), global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.PropertiesPostRequest.Parser, new[]{ "EntryId", "PropertyDictionary" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.PropertiesPostResponse), global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.PropertiesPostResponse.Parser, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class PropertiesGetRequest : pb::IMessage<PropertiesGetRequest> {
    private static readonly pb::MessageParser<PropertiesGetRequest> _parser = new pb::MessageParser<PropertiesGetRequest>(() => new PropertiesGetRequest());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<PropertiesGetRequest> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.PropertiesReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public PropertiesGetRequest() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public PropertiesGetRequest(PropertiesGetRequest other) : this() {
      EntryId = other.entryId_ != null ? other.EntryId.Clone() : null;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public PropertiesGetRequest Clone() {
      return new PropertiesGetRequest(this);
    }

    /// <summary>Field number for the "EntryId" field.</summary>
    public const int EntryIdFieldNumber = 1;
    private global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.Identifier entryId_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.Identifier EntryId {
      get { return entryId_; }
      set {
        entryId_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as PropertiesGetRequest);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(PropertiesGetRequest other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!object.Equals(EntryId, other.EntryId)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (entryId_ != null) hash ^= EntryId.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (entryId_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(EntryId);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (entryId_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(EntryId);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(PropertiesGetRequest other) {
      if (other == null) {
        return;
      }
      if (other.entryId_ != null) {
        if (entryId_ == null) {
          entryId_ = new global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.Identifier();
        }
        EntryId.MergeFrom(other.EntryId);
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            if (entryId_ == null) {
              entryId_ = new global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.Identifier();
            }
            input.ReadMessage(entryId_);
            break;
          }
        }
      }
    }

  }

  public sealed partial class PropertiesGetResponse : pb::IMessage<PropertiesGetResponse> {
    private static readonly pb::MessageParser<PropertiesGetResponse> _parser = new pb::MessageParser<PropertiesGetResponse>(() => new PropertiesGetResponse());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<PropertiesGetResponse> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.PropertiesReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public PropertiesGetResponse() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public PropertiesGetResponse(PropertiesGetResponse other) : this() {
      PropertyDictionary = other.propertyDictionary_ != null ? other.PropertyDictionary.Clone() : null;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public PropertiesGetResponse Clone() {
      return new PropertiesGetResponse(this);
    }

    /// <summary>Field number for the "PropertyDictionary" field.</summary>
    public const int PropertyDictionaryFieldNumber = 1;
    private global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.PropertyDictionary propertyDictionary_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.PropertyDictionary PropertyDictionary {
      get { return propertyDictionary_; }
      set {
        propertyDictionary_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as PropertiesGetResponse);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(PropertiesGetResponse other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!object.Equals(PropertyDictionary, other.PropertyDictionary)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (propertyDictionary_ != null) hash ^= PropertyDictionary.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (propertyDictionary_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(PropertyDictionary);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (propertyDictionary_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(PropertyDictionary);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(PropertiesGetResponse other) {
      if (other == null) {
        return;
      }
      if (other.propertyDictionary_ != null) {
        if (propertyDictionary_ == null) {
          propertyDictionary_ = new global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.PropertyDictionary();
        }
        PropertyDictionary.MergeFrom(other.PropertyDictionary);
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            if (propertyDictionary_ == null) {
              propertyDictionary_ = new global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.PropertyDictionary();
            }
            input.ReadMessage(propertyDictionary_);
            break;
          }
        }
      }
    }

  }

  public sealed partial class PropertiesPostRequest : pb::IMessage<PropertiesPostRequest> {
    private static readonly pb::MessageParser<PropertiesPostRequest> _parser = new pb::MessageParser<PropertiesPostRequest>(() => new PropertiesPostRequest());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<PropertiesPostRequest> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.PropertiesReflection.Descriptor.MessageTypes[2]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public PropertiesPostRequest() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public PropertiesPostRequest(PropertiesPostRequest other) : this() {
      EntryId = other.entryId_ != null ? other.EntryId.Clone() : null;
      PropertyDictionary = other.propertyDictionary_ != null ? other.PropertyDictionary.Clone() : null;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public PropertiesPostRequest Clone() {
      return new PropertiesPostRequest(this);
    }

    /// <summary>Field number for the "EntryId" field.</summary>
    public const int EntryIdFieldNumber = 1;
    private global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.Identifier entryId_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.Identifier EntryId {
      get { return entryId_; }
      set {
        entryId_ = value;
      }
    }

    /// <summary>Field number for the "PropertyDictionary" field.</summary>
    public const int PropertyDictionaryFieldNumber = 2;
    private global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.PropertyDictionary propertyDictionary_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.PropertyDictionary PropertyDictionary {
      get { return propertyDictionary_; }
      set {
        propertyDictionary_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as PropertiesPostRequest);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(PropertiesPostRequest other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!object.Equals(EntryId, other.EntryId)) return false;
      if (!object.Equals(PropertyDictionary, other.PropertyDictionary)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (entryId_ != null) hash ^= EntryId.GetHashCode();
      if (propertyDictionary_ != null) hash ^= PropertyDictionary.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (entryId_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(EntryId);
      }
      if (propertyDictionary_ != null) {
        output.WriteRawTag(18);
        output.WriteMessage(PropertyDictionary);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (entryId_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(EntryId);
      }
      if (propertyDictionary_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(PropertyDictionary);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(PropertiesPostRequest other) {
      if (other == null) {
        return;
      }
      if (other.entryId_ != null) {
        if (entryId_ == null) {
          entryId_ = new global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.Identifier();
        }
        EntryId.MergeFrom(other.EntryId);
      }
      if (other.propertyDictionary_ != null) {
        if (propertyDictionary_ == null) {
          propertyDictionary_ = new global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.PropertyDictionary();
        }
        PropertyDictionary.MergeFrom(other.PropertyDictionary);
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            if (entryId_ == null) {
              entryId_ = new global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.Identifier();
            }
            input.ReadMessage(entryId_);
            break;
          }
          case 18: {
            if (propertyDictionary_ == null) {
              propertyDictionary_ = new global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.PropertyDictionary();
            }
            input.ReadMessage(propertyDictionary_);
            break;
          }
        }
      }
    }

  }

  public sealed partial class PropertiesPostResponse : pb::IMessage<PropertiesPostResponse> {
    private static readonly pb::MessageParser<PropertiesPostResponse> _parser = new pb::MessageParser<PropertiesPostResponse>(() => new PropertiesPostResponse());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<PropertiesPostResponse> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.PropertiesReflection.Descriptor.MessageTypes[3]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public PropertiesPostResponse() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public PropertiesPostResponse(PropertiesPostResponse other) : this() {
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public PropertiesPostResponse Clone() {
      return new PropertiesPostResponse(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as PropertiesPostResponse);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(PropertiesPostResponse other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(PropertiesPostResponse other) {
      if (other == null) {
        return;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
