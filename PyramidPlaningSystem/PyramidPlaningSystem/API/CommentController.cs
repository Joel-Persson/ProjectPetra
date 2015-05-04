using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using PyramidPlaningSystem.Models;

namespace PyramidPlaningSystem.API
{
    public class CommentController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        public IEnumerable<Comment> GetCommentsByToDoId(Guid id)
        {
            var comments =  db.Comments.Where(x => x.ToDo.ToDoId == id).ToList();
            return comments;
        }

            // PUT: api/Comment/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutComment(Guid id, Comment comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != comment.CommentId)
            {
                return BadRequest();
            }

            db.Entry(comment).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Comment
        [HttpPost]
        public HttpResponseMessage PostComment(Comment comment)
        {
            if (ModelState.IsValid)
            {

                if (comment.CommentId == Guid.Empty)
                {
                    comment.Date = DateTime.Now;
                    db.Comments.Add(comment);
                    db.SaveChanges();
                }

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, comment.CommentId);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = comment.CommentId }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE: api/Comment/5
        [ResponseType(typeof(Comment))]
        public IHttpActionResult DeleteComment(Guid id)
        {
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return NotFound();
            }

            db.Comments.Remove(comment);
            db.SaveChanges();

            return Ok(comment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CommentExists(Guid id)
        {
            return db.Comments.Count(e => e.CommentId == id) > 0;
        }
    }
}