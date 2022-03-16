using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace View.Posts
{
    [DataContract]
    public sealed class PostUpdateInfo
    {
        [DataMember]
        [StringLength(100)]
        public string Title { get; set; }

        [DataMember]
        [StringLength(1000)]
        public string Text { get; set; }

        [DataMember]
        [MaxLength(10, ErrorMessage = "The field Tags must be an array type with a maximum length of '10'.")]
        public string[] Tags { get; set; }
    }
}
