using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StatTemplate
{
    [Serializable()]
    public class User
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string username = "";
        /// <summary>
        /// 密码
        /// </summary>
        public string secretcode = "";
        /// <summary>
        /// 权限、角色
        /// </summary>
        public int role = 0;
        /// <summary>
        /// 最后一次登录的时间
        /// </summary>
        public string logintime = "";
        /// <summary>
        /// 最后一次登录的电脑的ip地址
        /// </summary>
        public string loginip = "";
        /// <summary>
        /// 用户被创建的时间
        /// </summary>
        public string createtime = "";
    }

    /// <summary>
    /// 流程图结构
    /// </summary>
    [Serializable()]
    public class RoutingChart
    {
        public RoutingChart()
        {
            try
            {
                _routing = new List<Links> { };
            }
            catch (Exception ex){throw ex;}
        }

        #region 表结构
        string _name = "";
        /// <summary>
        /// 流程名称
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }

        string _issuer = "";
        /// <summary>
        /// 发行人员
        /// </summary>
        public string Issuer
        {
            set { _issuer = value; }
            get { return _issuer; }
        }

        string _createtime = "";
        /// <summary>
        /// 流程创建时间
        /// </summary>
        public string CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }

        string _modifytime = "";
        /// <summary>
        /// 修改时间
        /// </summary>
        public string ModifyTime
        {
            set { _modifytime = value; }
            get { return _modifytime; }
        }

        double _version = 1.00;
        /// <summary>
        /// 版本
        /// </summary>
        public double Version
        {
            set { _version = value; }
            get { return _version; }
        }

        string _remarkinfo = "";
        /// <summary>
        /// 备注信息
        /// </summary>
        public string RemarkInfo
        {
            set { _remarkinfo = value; }
            get { return _remarkinfo; }
        }

        List<Links> _routing = null;
        /// <summary>
        /// 该流程的全部路由
        /// </summary>
        public List<Links> Routing
        {
            set { _routing = value; }
            get { return _routing; }
        }
        #endregion

        List<string> _allLinksNames = null;
        /// <summary>
        /// 该流程的全部生产环节的名称集合
        /// </summary>
        public List<string> AllLinksNames
        {
            get 
            {
                _allLinksNames = new List<string> { };
                Links link = null;
                for (int count = 0; count < _routing.Count; count++)
                {
                    link = _routing[count];
                    _allLinksNames.AddRange(link.StationNameList);
                }

                return _allLinksNames;
            }
        }

        List<string> _allIpAddrs = null;
        /// <summary>
        /// 全部站点的ip地址
        /// </summary>
        public List<string> AllIpAddrs
        {
            get
            {
                _allIpAddrs = new List<string> { };
                Links link = null;
                for (int count = 0; count < _routing.Count; count++)
                {
                    link = _routing[count];
                    _allIpAddrs.AddRange(link.IPList);
                }

                return _allIpAddrs;
            }
        }

        List<string> _allMac = null;
        public List<string> AllMac
        {
            get
            {
                _allMac = new List<string> { };
                Links link = null;
                for (int count = 0; count < _routing.Count; count++)
                {
                    link = _routing[count];
                    _allMac.AddRange(link.MacList);
                }

                return _allMac;
            }
        }
    }

    /// <summary>
    /// 工序，一个工序可包含多个测试站
    /// </summary>
    [Serializable()]
    public class Links
    {
        public Links()
        {
            _testStationList = new List<TestStation> { };
            _ipList = new List<string> { };
            _macList = new List<string> { };
        }

        /// <summary>
        /// 该工序名称
        /// </summary>
        /// <param name="pLinksName"></param>
        public Links(string pLinksName)
        {
            _name = pLinksName;
            _testStationList = new List<TestStation> { };
            _ipList = new List<string> { };
            _macList = new List<string> { };
            _stationNameList = new List<string> { };
        }

        string _name = "";
        /// <summary>
        /// 工序名称
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }

        string _instructions = "";
        /// <summary>
        /// 设备说明书
        /// </summary>
        public string Instructions
        {
            set { _instructions = value; }
            get { return _instructions; }
        }

        string _sop = "";
        /// <summary>
        /// 设备操作说明书
        /// </summary>
        public string SOP
        {
            set { _sop = value; }
            get { return _sop; }
        }

        string _maintenancebook = "";
        /// <summary>
        /// 设备保养记录
        /// </summary>
        public string MaintenanceBook
        {
            set { _maintenancebook = value; }
            get { return _maintenancebook; }
        }

        /// <summary>
        /// 该工序的所有测试站列表
        /// </summary>
        List<TestStation> _testStationList = null;
        public List<TestStation> TestStationList
        {
            set { _testStationList = value; }
            get { return _testStationList; }
        }

        List<string> _ipList = null;
        /// <summary>
        /// 该工序的所有测试站电脑ip地址列表
        /// </summary>
        public List<string> IPList
        {
            get 
            {
                _ipList = new List<string> { };
                TestStation ts = null;
                for (int count = 0; count < _testStationList.Count; count++)
                { 
                    ts = _testStationList[count];
                    _ipList.Add(ts.IP);
                }

                return _ipList;
            }
        }

        List<string> _macList = null;
        /// <summary>
        /// 该工序的所有测试站电脑物理地址列表
        /// </summary>
        public List<string> MacList
        {
            get
            {
                _macList = new List<string> { };
                TestStation ts = null;
                for (int count = 0; count < _testStationList.Count; count++)
                {
                    ts = _testStationList[count];
                    _macList.Add(ts.Mac);
                }

                return _macList;
            }
        }

        List<string> _stationNameList = null;
        /// <summary>
        /// 该工序的所有测试站的名称，例如"打螺丝1/打螺丝2/点胶1"
        /// </summary>
        public List<string> StationNameList
        {
            get
            {
                _stationNameList = new List<string> { };
                TestStation ts = null;
                for (int count = 0; count < _testStationList.Count; count++)
                {
                    ts = _testStationList[count];
                    _stationNameList.Add(ts.Name);
                }

                return _stationNameList;
            }
        }
    }

    /// <summary>
    /// 测试站
    /// </summary>
    [Serializable()]
    public class TestStation
    {
        string _name = "";
        /// <summary>
        /// 测试站名称，例如"打螺丝1/打螺丝2/点胶1"
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }

        string _pcname = "";
        /// <summary>
        /// 测试站的电脑名称
        /// </summary>
        public string PCName
        {
            set { _pcname = value; }
            get { return _pcname; }
        }

        string _ip = "";
        /// <summary>
        /// 测试站ip地址
        /// </summary>
        public string IP
        {
            set { _ip = value; }
            get { return _ip; }
        }

        string _mac = "";
        /// <summary>
        /// 测试站网卡地址
        /// </summary>
        public string Mac
        {
            set { _mac = value; }
            get { return _mac;  }
        }

        string _remark = "";
        /// <summary>
        /// 备注信息
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
    }

    /// <summary>
    /// 生产线
    /// </summary>
    [Serializable()]
    public class ProductionLine
    {
        public ProductionLine()
        { 
        
        }
        
        string _name = "";
        /// <summary>
        /// 生产线名称
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }

        string _createtime = "";
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }

        string _modifytime = "";
        /// <summary>
        /// 修改时间
        /// </summary>
        public string ModifyTime
        {
            set { _modifytime = value; }
            get { return _modifytime; }
        }

        string _routingname = "";
        /// <summary>
        /// 该生产线所使用的流程名称
        /// </summary>
        public string RoutingName
        {
            set { _routingname = value; }
            get { return _routingname; }
        }

        double _routingversion = 0.0;
        /// <summary>
        /// 该生产线所使用的流程版本
        /// </summary>
        public double RoutingVersion
        {
            set { _routingversion = value; }
            get { return _routingversion; }
        }

        bool _isjoinmes = false;
        /// <summary>
        /// 是否连接到工厂MES
        /// </summary>
        public bool IsJoinMes
        {
            set { _isjoinmes = value; }
            get { return _isjoinmes; }
        }

        string _mesElements = "";
        /// <summary>
        /// 连接工厂MES的要素（站点、地址、格式等）
        /// </summary>
        public string MesElements
        {
            set { _mesElements = value; }
            get { return _mesElements; }
        }

        bool _siteCheckInRealtime = false;
        /// <summary>
        /// 是否实时检查过站产品
        /// 是: 产品到站以后，询问系统是否处在当前站位，根据是否处在当前站位做下一步动作
        /// 否：产品到站以后，直接测试，测试完成后上传测试结果
        /// </summary>
        public bool SiteCheckInRealTime
        {
            set { _siteCheckInRealtime = value; }
            get { return _siteCheckInRealtime; }
        }
    }
}
