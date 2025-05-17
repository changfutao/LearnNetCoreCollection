using LearnNetCore.Basic.DbContexts;

namespace LearnNetCore.Basic.Models
{
    public class BookEntity
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public DateTime PubTime { get; set; }
        public double Price { get; set; }
        public string AuthorName { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? ModifiedTime { get; set; }
    }
}
