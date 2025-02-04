namespace DevEval.Application.Products.Dtos
{
    public class RatingDto
    {
        /// <summary>
        /// Gets the average rating score (between 0 and 5).
        /// </summary>
        public double Rate { get; set; }

        /// <summary>
        /// Gets the number of votes for the rating.
        /// </summary>
        public int Count { get; set; }
    }
}
