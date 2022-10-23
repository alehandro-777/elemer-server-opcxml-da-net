using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpcXmlDaWcfService
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://opcfoundation.org/webservices/XMLDA/1.0/")]
    public class ReplyBase
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.DateTime RcvTime;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.DateTime ReplyTime;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ClientRequestHandle;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string RevisedLocaleID;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public serverState ServerState;
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://opcfoundation.org/webservices/XMLDA/1.0/")]
    public enum serverState
    {

        /// <remarks/>
        running,

        /// <remarks/>
        failed,

        /// <remarks/>
        noConfig,

        /// <remarks/>
        suspended,

        /// <remarks/>
        test,

        /// <remarks/>
        commFault,
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://opcfoundation.org/webservices/XMLDA/1.0/")]
    public class PropertyReplyList
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Properties")]
        public ItemProperty[] Properties;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ItemPath;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ItemName;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.Xml.XmlQualifiedName ResultID;
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://opcfoundation.org/webservices/XMLDA/1.0/")]
    public class ItemProperty
    {

        /// <remarks/>
        public object Value;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.Xml.XmlQualifiedName Name;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Description;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ItemPath;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ItemName;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.Xml.XmlQualifiedName ResultID;
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://opcfoundation.org/webservices/XMLDA/1.0/")]
    public class ItemIdentifier
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ItemPath;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ItemName;
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://opcfoundation.org/webservices/XMLDA/1.0/")]
    public class BrowseElement
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Properties")]
        public ItemProperty[] Properties;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ItemPath;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ItemName;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool IsItem;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool HasChildren;
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://opcfoundation.org/webservices/XMLDA/1.0/")]
    public class SubscribePolledRefreshReplyItemList
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Items")]
        public ItemValue[] Items;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string SubscriptionHandle;
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://opcfoundation.org/webservices/XMLDA/1.0/")]
    public class ItemValue
    {

        /// <remarks/>
        public string DiagnosticInfo;

        /// <remarks/>
        public object Value;

        /// <remarks/>
        public OPCQuality Quality;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ItemPath;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ItemName;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ClientItemHandle;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.DateTime Timestamp;

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TimestampSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.Xml.XmlQualifiedName ResultID;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.Xml.XmlQualifiedName ValueTypeQualifier;
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://opcfoundation.org/webservices/XMLDA/1.0/")]
    public class OPCQuality
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute(qualityBits.good)]
        public qualityBits QualityField = qualityBits.good;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute(limitBits.none)]
        public limitBits LimitField = limitBits.none;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute(typeof(System.Byte), "0")]
        public System.Byte VendorField = ((System.Byte)(0));
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://opcfoundation.org/webservices/XMLDA/1.0/")]
    public enum qualityBits
    {

        /// <remarks/>
        bad,

        /// <remarks/>
        badConfigurationError,

        /// <remarks/>
        badNotConnected,

        /// <remarks/>
        badDeviceFailure,

        /// <remarks/>
        badSensorFailure,

        /// <remarks/>
        badLastKnownValue,

        /// <remarks/>
        badCommFailure,

        /// <remarks/>
        badOutOfService,

        /// <remarks/>
        badWaitingForInitialData,

        /// <remarks/>
        uncertain,

        /// <remarks/>
        uncertainLastUsableValue,

        /// <remarks/>
        uncertainSensorNotAccurate,

        /// <remarks/>
        uncertainEUExceeded,

        /// <remarks/>
        uncertainSubNormal,

        /// <remarks/>
        good,

        /// <remarks/>
        goodLocalOverride,
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://opcfoundation.org/webservices/XMLDA/1.0/")]
    public enum limitBits
    {

        /// <remarks/>
        none,

        /// <remarks/>
        low,

        /// <remarks/>
        high,

        /// <remarks/>
        constant,
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://opcfoundation.org/webservices/XMLDA/1.0/")]
    public class SubscribeItemValue
    {

        /// <remarks/>
        public ItemValue ItemValue;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int RevisedSamplingRate;

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool RevisedSamplingRateSpecified;
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://opcfoundation.org/webservices/XMLDA/1.0/")]
    public class SubscribeReplyItemList
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Items")]
        public SubscribeItemValue[] Items;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int RevisedSamplingRate;

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool RevisedSamplingRateSpecified;
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://opcfoundation.org/webservices/XMLDA/1.0/")]
    public class SubscribeRequestItem
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ItemPath;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.Xml.XmlQualifiedName ReqType;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ItemName;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ClientItemHandle;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.Single Deadband;

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DeadbandSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int RequestedSamplingRate;

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool RequestedSamplingRateSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool EnableBuffering;

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool EnableBufferingSpecified;
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://opcfoundation.org/webservices/XMLDA/1.0/")]
    public class SubscribeRequestItemList
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Items")]
        public SubscribeRequestItem[] Items;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ItemPath;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.Xml.XmlQualifiedName ReqType;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.Single Deadband;

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DeadbandSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int RequestedSamplingRate;

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool RequestedSamplingRateSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool EnableBuffering;

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool EnableBufferingSpecified;
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://opcfoundation.org/webservices/XMLDA/1.0/")]
    public class WriteRequestItemList
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Items")]
        public ItemValue[] Items;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ItemPath;
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://opcfoundation.org/webservices/XMLDA/1.0/")]
    public class OPCError
    {

        /// <remarks/>
        public string Text;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.Xml.XmlQualifiedName ID;
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://opcfoundation.org/webservices/XMLDA/1.0/")]
    public class ReplyItemList
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Items")]
        public ItemValue[] Items;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Reserved;
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://opcfoundation.org/webservices/XMLDA/1.0/")]
    public class ReadRequestItem
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ItemPath;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.Xml.XmlQualifiedName ReqType;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ItemName;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ClientItemHandle;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int MaxAge;

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool MaxAgeSpecified;
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://opcfoundation.org/webservices/XMLDA/1.0/")]
    public class ReadRequestItemList
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Items")]
        public ReadRequestItem[] Items;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ItemPath;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.Xml.XmlQualifiedName ReqType;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int MaxAge;

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool MaxAgeSpecified;
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://opcfoundation.org/webservices/XMLDA/1.0/")]
    public class RequestOptions
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.DateTime RequestDeadline;

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool RequestDeadlineSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute(true)]
        public bool ReturnErrorText = true;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute(false)]
        public bool ReturnDiagnosticInfo = false;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute(false)]
        public bool ReturnItemTime = false;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute(false)]
        public bool ReturnItemPath = false;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute(false)]
        public bool ReturnItemName = false;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ClientRequestHandle;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string LocaleID;
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://opcfoundation.org/webservices/XMLDA/1.0/")]
    public class ServerStatus
    {

        /// <remarks/>
        public string StatusInfo;

        /// <remarks/>
        public string VendorInfo;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SupportedLocaleIDs")]
        public string[] SupportedLocaleIDs;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SupportedInterfaceVersions")]
        public interfaceVersion[] SupportedInterfaceVersions;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.DateTime StartTime;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ProductVersion;
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://opcfoundation.org/webservices/XMLDA/1.0/")]
    public enum interfaceVersion
    {

        /// <remarks/>
        XML_DA_Version_1_0,
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://opcfoundation.org/webservices/XMLDA/1.0/")]
    public enum browseFilter
    {

        /// <remarks/>
        all,

        /// <remarks/>
        branch,

        /// <remarks/>
        item,
    }

}