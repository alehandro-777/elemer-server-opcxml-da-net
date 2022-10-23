using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace OpcXmlDaWcfService
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace = "http://opcfoundation.org/webservices/XMLDA/1.0/", ConfigurationName = "Service")]
    public interface IOpcXmlDaService
    {
        //**************************** ADDED ******************************************************************
        [OperationContract]
        string GetLogMessage(int index);
        [OperationContract]
        int GetLogMessageSize();
        //*********************************************************************************************XML DA 10
        [System.ServiceModel.OperationContractAttribute(Action = "http://opcfoundation.org/webservices/XMLDA/1.0/GetStatus", ReplyAction = "*")]
        [System.ServiceModel.XmlSerializerFormatAttribute()]
        ReplyBase GetStatus([System.Xml.Serialization.XmlAttributeAttribute()] string LocaleID, [System.Xml.Serialization.XmlAttributeAttribute()] string ClientRequestHandle, out ServerStatus Status);

        [System.ServiceModel.OperationContractAttribute(Action = "http://opcfoundation.org/webservices/XMLDA/1.0/Read", ReplyAction = "*")]
        [System.ServiceModel.XmlSerializerFormatAttribute()]
        ReplyBase Read(RequestOptions Options, ReadRequestItemList ItemList, out ReplyItemList RItemList, [System.Xml.Serialization.XmlElementAttribute("Errors")] out OPCError[] Errors);

        [System.ServiceModel.OperationContractAttribute(Action = "http://opcfoundation.org/webservices/XMLDA/1.0/Write", ReplyAction = "*")]
        [System.ServiceModel.XmlSerializerFormatAttribute()]
        ReplyBase Write(RequestOptions Options, WriteRequestItemList ItemList, [System.Xml.Serialization.XmlAttributeAttribute()] bool ReturnValuesOnReply, out ReplyItemList RItemList, [System.Xml.Serialization.XmlElementAttribute("Errors")] out OPCError[] Errors);

        [System.ServiceModel.OperationContractAttribute(Action = "http://opcfoundation.org/webservices/XMLDA/1.0/Subscribe", ReplyAction = "*")]
        [System.ServiceModel.XmlSerializerFormatAttribute()]
        ReplyBase Subscribe(RequestOptions Options, SubscribeRequestItemList ItemList, [System.Xml.Serialization.XmlAttributeAttribute()] bool ReturnValuesOnReply, [System.Xml.Serialization.XmlAttributeAttribute()] [System.ComponentModel.DefaultValueAttribute(0)] int SubscriptionPingRate, out SubscribeReplyItemList RItemList, [System.Xml.Serialization.XmlElementAttribute("Errors")] out OPCError[] Errors, [System.Xml.Serialization.XmlAttributeAttribute()] out string ServerSubHandle);

        [System.ServiceModel.OperationContractAttribute(Action = "http://opcfoundation.org/webservices/XMLDA/1.0/SubscriptionPolledRefresh", ReplyAction = "*")]
        [System.ServiceModel.XmlSerializerFormatAttribute()]
        ReplyBase SubscriptionPolledRefresh(RequestOptions Options, [System.Xml.Serialization.XmlElementAttribute("ServerSubHandles")] string[] ServerSubHandles, [System.Xml.Serialization.XmlAttributeAttribute()] System.DateTime HoldTime, [System.Xml.Serialization.XmlAttributeAttribute()] [System.Xml.Serialization.XmlIgnoreAttribute()] bool HoldTimeSpecified, [System.Xml.Serialization.XmlAttributeAttribute()] [System.ComponentModel.DefaultValueAttribute(0)] int WaitTime, [System.Xml.Serialization.XmlAttributeAttribute()] [System.ComponentModel.DefaultValueAttribute(false)] bool ReturnAllItems, [System.Xml.Serialization.XmlElementAttribute("InvalidServerSubHandles")] out string[] InvalidServerSubHandles, [System.Xml.Serialization.XmlElementAttribute("RItemList")] out SubscribePolledRefreshReplyItemList[] RItemList, [System.Xml.Serialization.XmlElementAttribute("Errors")] out OPCError[] Errors, [System.Xml.Serialization.XmlAttributeAttribute()] [System.ComponentModel.DefaultValueAttribute(false)] out bool DataBufferOverflow);

        [System.ServiceModel.OperationContractAttribute(Action = "http://opcfoundation.org/webservices/XMLDA/1.0/SubscriptionCancel", ReplyAction = "*")]
        [System.ServiceModel.XmlSerializerFormatAttribute()]
        void SubscriptionCancel([System.Xml.Serialization.XmlAttributeAttribute()] string ServerSubHandle, [System.Xml.Serialization.XmlAttributeAttribute()] ref string ClientRequestHandle);

        [System.ServiceModel.OperationContractAttribute(Action = "http://opcfoundation.org/webservices/XMLDA/1.0/Browse", ReplyAction = "*")]
        [System.ServiceModel.XmlSerializerFormatAttribute()]
        ReplyBase Browse(
                    [System.Xml.Serialization.XmlElementAttribute("PropertyNames")] System.Xml.XmlQualifiedName[] PropertyNames,
                    [System.Xml.Serialization.XmlAttributeAttribute()] string LocaleID,
                    [System.Xml.Serialization.XmlAttributeAttribute()] string ClientRequestHandle,
                    [System.Xml.Serialization.XmlAttributeAttribute()] string ItemPath,
                    [System.Xml.Serialization.XmlAttributeAttribute()] string ItemName,
                    [System.Xml.Serialization.XmlAttributeAttribute()] ref string ContinuationPoint,
                    [System.Xml.Serialization.XmlAttributeAttribute()] [System.ComponentModel.DefaultValueAttribute(0)] int MaxElementsReturned,
                    [System.Xml.Serialization.XmlAttributeAttribute()] [System.ComponentModel.DefaultValueAttribute(browseFilter.all)] browseFilter BrowseFilter,
                    [System.Xml.Serialization.XmlAttributeAttribute()] string ElementNameFilter,
                    [System.Xml.Serialization.XmlAttributeAttribute()] string VendorFilter,
                    [System.Xml.Serialization.XmlAttributeAttribute()] [System.ComponentModel.DefaultValueAttribute(false)] bool ReturnAllProperties,
                    [System.Xml.Serialization.XmlAttributeAttribute()] [System.ComponentModel.DefaultValueAttribute(false)] bool ReturnPropertyValues,
                    [System.Xml.Serialization.XmlAttributeAttribute()] [System.ComponentModel.DefaultValueAttribute(false)] bool ReturnErrorText,
                    [System.Xml.Serialization.XmlElementAttribute("Elements")] out BrowseElement[] Elements,
                    [System.Xml.Serialization.XmlElementAttribute("Errors")] out OPCError[] Errors,
                    [System.Xml.Serialization.XmlAttributeAttribute()] [System.ComponentModel.DefaultValueAttribute(false)] out bool MoreElements);

        [System.ServiceModel.OperationContractAttribute(Action = "http://opcfoundation.org/webservices/XMLDA/1.0/GetProperties", ReplyAction = "*")]
        [System.ServiceModel.XmlSerializerFormatAttribute()]
        ReplyBase GetProperties([System.Xml.Serialization.XmlElementAttribute("ItemIDs")] ItemIdentifier[] ItemIDs, [System.Xml.Serialization.XmlElementAttribute("PropertyNames")] System.Xml.XmlQualifiedName[] PropertyNames, [System.Xml.Serialization.XmlAttributeAttribute()] string LocaleID, [System.Xml.Serialization.XmlAttributeAttribute()] string ClientRequestHandle, [System.Xml.Serialization.XmlAttributeAttribute()] string ItemPath, [System.Xml.Serialization.XmlAttributeAttribute()] [System.ComponentModel.DefaultValueAttribute(false)] bool ReturnAllProperties, [System.Xml.Serialization.XmlAttributeAttribute()] [System.ComponentModel.DefaultValueAttribute(false)] bool ReturnPropertyValues, [System.Xml.Serialization.XmlAttributeAttribute()] [System.ComponentModel.DefaultValueAttribute(false)] bool ReturnErrorText, [System.Xml.Serialization.XmlElementAttribute("PropertyLists")] out PropertyReplyList[] PropertyLists, [System.Xml.Serialization.XmlElementAttribute("Errors")] out OPCError[] Errors);
    }
}
