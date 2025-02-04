using DevEval.Common;

namespace DevEval.Domain.ValueObjects
{
    /// <summary>
    /// Represents the rating details of a product as a value object.
    /// </summary>
    public class Rating : ValueObject
    {
        /// <summary>
        /// Gets the average rating score (between 0 and 5).
        /// </summary>
        public double Rate { get; }

        /// <summary>
        /// Gets the number of votes for the rating.
        /// </summary>
        public int Count { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Rating"/> class.
        /// </summary>
        /// <param name="rate">The average rating score (0-5).</param>
        /// <param name="count">The total number of votes.</param>
        public Rating(double rate, int count)
        {
            if (rate < 0 || rate > 5)
                throw new ArgumentOutOfRangeException(nameof(rate), "Rate must be between 0 and 5.");

            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count), "Count must be greater than or equal to zero.");

            Rate = rate;
            Count = count;
        }

        private Rating()
        {
            Rate = 0.0;
            Count = 0;
        }

        /// <summary>
        /// Represents an empty rating instance.
        /// </summary>
        public static Rating Empty => new Rating(0, 0);

        /// <summary>
        /// Creates a new Rating instance with an additional vote.
        /// </summary>
        /// <param name="newRate">The new rating given (0-5).</param>
        /// <returns>A new <see cref="Rating"/> instance with the updated values.</returns>
        public Rating WithNewVote(double newRate)
        {
            if (newRate < 0 || newRate > 5)
                throw new ArgumentOutOfRangeException(nameof(newRate), "New rate must be between 0 and 5.");

            double totalScore = (Rate * Count) + newRate;
            int newCount = Count + 1;
            double newAverage = totalScore / newCount;

            return new Rating(newAverage, newCount);
        }

        /// <summary>
        /// Returns a string representation of the rating.
        /// </summary>
        public override string ToString() => $"{Rate:N1} ({Count} votes)";

        /// <summary>
        /// Defines the components of equality for the value object.
        /// </summary>
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Rate;
            yield return Count;
        }
    }
}