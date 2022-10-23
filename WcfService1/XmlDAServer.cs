using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Configuration;

using PollingProccessSupport;
using ElemerDriver;

namespace OpcXmlDaWcfService
{
    //Просто класс - обертка чтобы не лазить в класс службы
    public class XmlDAServer : IOpcXmlDaService
    {
        private IOServer ioserver;

        public XmlDAServer()
        {
            IOServerConfig cfg = ElemerConfig.ReadIOServerConfig();
            ioserver = new IOServer(cfg);
            ioserver.Start();
        }

        //***************************
        public string GetLogMessage(int index)
        {
            return LogConsole.GetLogMessage(index);
        }

        public int GetLogMessageSize()
        {
            return LogConsole.GetLogMessageSize();
        }

        //*********************** OPC XML DA 1.0
        public ReplyBase GetStatus(string LocaleID, string ClientRequestHandle, out ServerStatus Status)
        {
            Status = new ServerStatus();
            Status.StartTime = DateTime.Now;
            Status.SupportedInterfaceVersions = new interfaceVersion[1];
            Status.SupportedInterfaceVersions[0] = interfaceVersion.XML_DA_Version_1_0;
            ReplyBase replay = new ReplyBase();
            replay.ClientRequestHandle = ClientRequestHandle;
            replay.RcvTime = DateTime.Now;
            replay.ReplyTime = DateTime.Now;
            replay.ServerState = serverState.running;
            replay.RevisedLocaleID = LocaleID;
            return replay;
        }
        public ReplyBase Read(RequestOptions Options, ReadRequestItemList ItemList, out ReplyItemList RItemList, out OPCError[] Errors)
        {
            Errors = null;
            ReplyBase replay = new ReplyBase();
            RItemList = new ReplyItemList();

            if (Options == null || ItemList == null)
            {
                Errors = new OPCError[] { new OPCError() };
                Errors[0].Text = "Error read request";
                return replay;
            }

            replay.ClientRequestHandle = Options.ClientRequestHandle;
            replay.RcvTime = DateTime.Now;

            RItemList.Items = new ItemValue[ItemList.Items.Length];
            int itemCounter = 0;

            foreach (ReadRequestItem reqItem in ItemList.Items)
            {
                DbItem ioData = null;
          
                try
                {
                    ioData = ioserver.ReadData(reqItem.ItemName);
                }
                catch (Exception)
                {
                    
                }
                
                
                ItemValue opcData = new ItemValue();
                opcData.ItemName = reqItem.ItemName;
                opcData.ClientItemHandle = reqItem.ClientItemHandle;
                opcData.Quality = new OPCQuality();
                opcData.TimestampSpecified = true;

                if (ioData != null)
                {
                    try
                    {
                        opcData.Value = ioData.Value;
                        opcData.Timestamp = ioData.LastUpdate;
                    }
                    catch (Exception)
                    {

                    }

                }
                else
                {
                    opcData.Value = 0;
                    opcData.Timestamp = DateTime.Now;
                    opcData.Quality.QualityField = qualityBits.badConfigurationError;

                    RItemList.Items[itemCounter] = opcData;
                    itemCounter++;

                    continue;
                }


                switch (ioData.CurrentQuality)
                {
                    case 0:
                        opcData.Quality.QualityField = qualityBits.badWaitingForInitialData;
                        break;
                    case 192:
                        opcData.Quality.QualityField = qualityBits.good;
                        break;
                    case 0x62:
                        opcData.Quality.QualityField = qualityBits.badNotConnected;
                        break;
                    case 0x39:
                        opcData.Quality.QualityField = qualityBits.badSensorFailure;
                        break;
                    case -1000:
                        opcData.Quality.QualityField = qualityBits.badSensorFailure;
                        break;
                    case -1:
                        opcData.Quality.QualityField = qualityBits.badCommFailure;
                        break;

                    default:
                        opcData.Quality.QualityField = qualityBits.badConfigurationError;
                        break;
                }
                

                RItemList.Items[itemCounter] = opcData;
                itemCounter++;
            }

            replay.ReplyTime = DateTime.Now;
            replay.ServerState = serverState.running;
            return replay;
        }
        public ReplyBase Write(RequestOptions Options, WriteRequestItemList ItemList, bool ReturnValuesOnReply, out ReplyItemList RItemList, out OPCError[] Errors)
        {
            RItemList = new ReplyItemList();
            Errors = null;
            ReplyBase replay = new ReplyBase();
            replay.RcvTime = DateTime.Now;
            replay.ClientRequestHandle = Options.ClientRequestHandle;

            if (Options == null || ItemList == null)
            {
                Errors = new OPCError[] { new OPCError() };
                Errors[0].Text = "Error write request";
                return replay;
            }

            if (ReturnValuesOnReply)
            {
                RItemList.Items = ItemList.Items;
            }

            foreach (ItemValue value in ItemList.Items)
            {
                //There is NO write
                //IODevice.WriteItem(value);
            }

            replay.ReplyTime = DateTime.Now;
            replay.ServerState = serverState.running;
            return replay;
        }


        public ReplyBase Subscribe(RequestOptions Options, SubscribeRequestItemList ItemList, bool ReturnValuesOnReply, int SubscriptionPingRate, out SubscribeReplyItemList RItemList, out OPCError[] Errors, out string ServerSubHandle)
        {
            RItemList = new SubscribeReplyItemList();

            Errors = null;
            ServerSubHandle = "ServerSubHandle";
            ReplyBase replay = new ReplyBase();
            replay.ClientRequestHandle = Options.ClientRequestHandle;
            replay.RcvTime = DateTime.Now;
            replay.ReplyTime = DateTime.Now;
            replay.ServerState = serverState.running;
            return replay;
        }
        public ReplyBase SubscriptionPolledRefresh(RequestOptions Options, string[] ServerSubHandles, System.DateTime HoldTime, bool HoldTimeSpecified, int WaitTime, bool ReturnAllItems, out string[] InvalidServerSubHandles, out SubscribePolledRefreshReplyItemList[] RItemList, out OPCError[] Errors, out bool DataBufferOverflow)
        {
            RItemList = null;
            Errors = null;
            InvalidServerSubHandles = null;
            DataBufferOverflow = false;
            ReplyBase replay = new ReplyBase();
            replay.ClientRequestHandle = Options.ClientRequestHandle;
            replay.RcvTime = DateTime.Now;
            replay.ReplyTime = DateTime.Now;
            replay.ServerState = serverState.running;
            return replay;
        }
        public void SubscriptionCancel(string ServerSubHandle, ref string ClientRequestHandle)
        {

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
            Elements = null;
            Errors = null;
            MoreElements = false;
            return null;
        }
        public ReplyBase GetProperties(ItemIdentifier[] ItemIDs, System.Xml.XmlQualifiedName[] PropertyNames, string LocaleID, string ClientRequestHandle, string ItemPath, bool ReturnAllProperties, bool ReturnPropertyValues, bool ReturnErrorText, out PropertyReplyList[] PropertyLists, out OPCError[] Errors)
        {
            PropertyLists = null;
            Errors = null;
            ReplyBase replay = new ReplyBase();
            replay.ClientRequestHandle = ClientRequestHandle;
            replay.RcvTime = DateTime.Now;
            replay.ReplyTime = DateTime.Now;
            replay.ServerState = serverState.running;
            return replay;
        }    


    }
}