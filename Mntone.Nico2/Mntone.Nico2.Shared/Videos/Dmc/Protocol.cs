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
            public ParametersInfo Parameters { get; set; }
        }

        [DataContract]
        public class HlsEncryptionV1
        {

            [DataMember(Name = "encrypted_key")]
            public string EncryptedKey { get; set; }

            [DataMember(Name = "key_uri")]
            public string KeyUri { get; set; }
        }

        [DataContract]
        public class Encryption
        {

            [DataMember(Name = "hls_encryption_v1")]
            public HlsEncryptionV1 HlsEncryptionV1 { get; set; }
        }

        [DataContract]
        public class HlsParameters
        {

            [DataMember(Name = "use_well_known_port")]
            public string UseWellKnownPort { get; set; }

            [DataMember(Name = "use_ssl")]
            public string UseSsl { get; set; }

            [DataMember(Name = "transfer_preset")]
            public string TransferPreset { get; set; }

            [DataMember(Name = "segment_duration")]
            public int SegmentDuration { get; set; }

            [DataMember(Name = "encryption")]
            public Encryption Encryption { get; set; }



            [DataMember(Name = "total_duration")]
            public int? TotalDuration { get; set; }

            [DataMember(Name = "media_segment_format")]
            public string MediaSegmentFormat { get; set; }

        }

        [DataContract]
        public class ParametersInfo
        {

            [DataMember(Name = "hls_parameters")]
            public HlsParameters HlsParameters { get; set; }

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
            public string TransferPreset { get; set; } = "";

            // res req
            [DataMember(Name = "use_ssl")]
            public string UseSsl { get; set; } = "yes";

            // res req
            [DataMember(Name = "use_well_known_port")]
            public string UseWellKnownPort { get; set; } = "yes";
        }
    }

    


}
