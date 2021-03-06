﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace ServerCore.DataModel
{
    /// <summary>
    /// The response to a particular submission for a given puzzle
    /// </summary>
    public class Response
    {
        public Response()
        {
        }

        public Response(Response source)
        {
            // do not fill out the ID
            Puzzle = source.Puzzle;
            IsSolution = source.IsSolution;
            SubmittedText = source.SubmittedText;
            ResponseText = source.ResponseText;
            Note = source.Note;
        }

        [NotMapped]
        private string submittedText = string.Empty;

        /// <summary>
        /// The Id
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        
        /// <summary>
        /// The id of the puzzle the response is for
        /// </summary>
        public int PuzzleID { get; set; }

        /// <summary>
        /// The puzzle the response is for
        /// </summary>
        public virtual Puzzle Puzzle { get; set; }

        /// <summary>
        /// True if the submitted text is considered a solution to the puzzle
        /// </summary>
        public bool IsSolution { get; set; }

        /// <summary>
        /// The submitted text
        /// </summary>
        [Required]
        public string SubmittedText
        {
            get { return submittedText; }
            set { submittedText = FormatSubmission(value); }
        }

        /// <summary>
        /// The response text
        /// </summary>
        [Required]
        public string ResponseText { get; set; }

        /// <summary>
        /// Any additional notes from the author
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Converts the submission to the common spaceless uppercase format used by the website
        /// </summary>
        /// <param name="submission">The submission text</param>
        /// <returns>The formatted submission</returns>
        public static string FormatSubmission(string submission)
        {
            if (submission == null)
            {
                return string.Empty;
            }

            return Regex.Replace(submission, @"[^a-zA-Z\d]", string.Empty).ToUpper();
        }
    }
}
