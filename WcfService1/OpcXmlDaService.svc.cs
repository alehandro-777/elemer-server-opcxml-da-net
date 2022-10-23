using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace OpcXmlDaWcfService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class OpcXmlDaService : IOpcXmlDaService
    {

        static OpcXmlDaService()
        { }

        private static readonly IOpcXmlDaService _server = new XmlDAServer();

        //*********************************** ADDED ADDED *****************
        public string GetLogMessage(int index)
        {
            return LogConsole.GetLogMessage(index);
        }

        public int GetLogMessageSize()
        {
           return LogConsole.GetLogMessageSize();
        }

        //**************** OPC XML DA 1.0   ********************************
        public ReplyBase GetStatus(string LocaleID, string ClientRequestHandle, out ServerStatus Status)
        {
            return _server.GetStatus(LocaleID, ClientRequestHandle, out Status);
        }

        public ReplyBase Read(RequestOptions Options, ReadRequestItemList ItemList, out ReplyItemList RItemList,  out OPCError[] Errors)
        {
            return _server.Read(Options, ItemList, out  RItemList, out  Errors);
        }
        public ReplyBase Write(RequestOptions Options, WriteRequestItemList ItemList,  bool ReturnValuesOnReply, out ReplyItemList RItemList,  out OPCError[] Errors)
        {
            return _server.Write(Options, ItemList, ReturnValuesOnReply, out RItemList, out Errors);
        }
        public ReplyBase Subscribe(RequestOptions Options, SubscribeRequestItemList ItemList,  bool ReturnValuesOnReply,  int SubscriptionPingRate, out SubscribeReplyItemList RItemList, out OPCError[] Errors, out string ServerSubHandle)
        {
            return _server.Subscribe( Options, ItemList, ReturnValuesOnReply,SubscriptionPingRate, out RItemList, out  Errors, out ServerSubHandle);
        }
        public ReplyBase SubscriptionPolledRefresh(RequestOptions Options, string[] ServerSubHandles,  System.DateTime HoldTime, bool HoldTimeSpecified,   int WaitTime, bool ReturnAllItems, out string[] InvalidServerSubHandles, out SubscribePolledRefreshReplyItemList[] RItemList, out OPCError[] Errors, out bool DataBufferOverflow)
        {
            return _server.SubscriptionPolledRefresh( Options,ServerSubHandles,  HoldTime, HoldTimeSpecified, WaitTime, ReturnAllItems, out InvalidServerSubHandles, out  RItemList, out  Errors, out  DataBufferOverflow);
        }
        public void SubscriptionCancel( string ServerSubHandle, ref string ClientRequestHandle)
        { 
            _server.SubscriptionCancel(ServerSubHandle, ref ClientRequestHandle);
        }
        public ReplyBase Browse(
                     System.Xml.XmlQualifiedName[] PropertyNames,
                     string LocaleID,
                     string ClientRequestHandle,
                     string ItemPath,
                     string ItemName,
                     ref string ContinuationPoint,
                     int MaxElementsReturned,
                     browseFilter BrowseFilter,
                     string ElementNameFilter,
                     string VendorFilter,
                     bool ReturnAllProperties,
                     bool ReturnPropertyValues,
                     bool ReturnErrorText,
                     out BrowseElement[] Elements,
                     out OPCError[] Errors,
                     out bool MoreElements)
        {
            return _server.Browse( PropertyNames, LocaleID, ClientRequestHandle, ItemPath, ItemName, ref  ContinuationPoint, MaxElementsReturned, BrowseFilter, ElementNameFilter, VendorFilter,
                     ReturnAllProperties,
                     ReturnPropertyValues,
                     ReturnErrorText,
                     out Elements,
                     out Errors,
                     out MoreElements);
        }
        public ReplyBase GetProperties( ItemIdentifier[] ItemIDs,  System.Xml.XmlQualifiedName[] PropertyNames,  string LocaleID,  string ClientRequestHandle,  string ItemPath, bool ReturnAllProperties, bool ReturnPropertyValues, bool ReturnErrorText, out PropertyReplyList[] PropertyLists, out OPCError[] Errors)
        {
            return _server.GetProperties(ItemIDs, PropertyNames, LocaleID, ClientRequestHandle, ItemPath, ReturnAllProperties, ReturnPropertyValues, ReturnErrorText, out  PropertyLists, out Errors);
        }    
    }
}
