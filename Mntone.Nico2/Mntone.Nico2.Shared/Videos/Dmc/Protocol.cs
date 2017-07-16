using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mntone.Nico2.Videos.Dmc
{
    [DataContract]
    public class Protocol
    {

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "parameters")]
        public ProtocolParameters Parameters { get; set; }
    }

    [DataContract]
    public class ProtocolParameters
    {

        [DataMember(Name = "http_parameters")]
        public HttpParameters HttpParameters { get; set; }
    }

    [DataContract]
    public class HttpParameters
    {

        [DataMember(Name = "method")]
        public string Method { get; set; }

        [DataMember(Name = "parameters")]
        public Parameters Parameters { get; set; }
    }

    [DataContract]
    public class Parameters
    {

        [DataMember(Name = "http_output_download_parameters")]
        public HttpOutputDownloadParameters HttpOutputDownloadParameters { get; set; }
    }
    
    [DataContract]
    public class HttpOutputDownloadParameters
    {
        // res
        [DataMember(Name = "file_extension")]
        public string FileExtension { get; set; }

        // res
        [DataMember(Name = "transfer_preset")]
        public string TransferPreset { get; set; }

        // res req
        [DataMember(Name = "use_ssl")]
        public string UseSsl { get; set; }

        // res req
        [DataMember(Name = "use_well_known_port")]
        public string UseWellKnownPort { get; set; }
    }
}
