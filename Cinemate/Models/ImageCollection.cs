namespace Cinemate.Models
{
    public class ImageCollection
    {
        public IEnumerable<string> GetImgCollection01()
        {
            return new List<string>
            {
                "image1.png", "image2.png", "image3.png", "image4.png", "image5.png", "image6.png", "image7.png"
            };
        }

        public IEnumerable<string> GetImgCollection02()
        {
            return new List<string>
            {
                "image8.png", "image9.png", "image10.png", "image11.png", "image12.png", "image13.png", "image14.png"
            };
        }

        public IEnumerable<string> GetImgCollection03()
        {
            return new List<string>
            {
                "image15.png", "image16.png", "image17.png", "image18.png", "image19.png", "image20.png", "image21.png"
            };
        }
    }
}