using System.Reflection.Metadata.Ecma335;
using AspNetCoreGeneratedDocument;
using Filminurk.ApplicationServices.Services;
using Filminurk.Core.dto;
using Filminurk.Core.ServiceInterface;
using Filminurk.Data;
using Filminurk.Models.UserComments;
using Microsoft.AspNetCore.Mvc;

namespace Filminurk.Controllers
{
    public class UserCommentsController : Controller
    {
        private readonly FilminurkTARpe24Context _context;
        private readonly IUserCommentsServices _userCommentsServices;

        public UserCommentsController
            (
            FilminurkTARpe24Context context
            , IUserCommentsServices userCommentsServices
            )
        {
            _context = context;
            _userCommentsServices = userCommentsServices;

        }
        public IActionResult Index()
        {
            var result = _context.UserComments
            .Select(c => new UserCommentsIndexViewModel
            {
                CommentID = (Guid)c.CommentID,
                CommentBody = c.CommentBody,
                IsHarmful = (int)c.IsHarmful,
                CommentCreatedAt = (DateTime)c.CommentCreatedAt,



            }
            );
            return View(result);
        }
        [HttpGet]
        public IActionResult NewComment()
        {
            //TODO erista kas tegemist on admini või tavakasutajaga
            UserCommentsCreateViewModel newcomment = new();
            return View();
        }
        [HttpPost, ActionName("NewComment")]
        //meetodile ei tohi panna allowanonymus
        public async Task<IActionResult> NewCommentPost(UserCommentsCreateViewModel newcommentVM)
        {
            //newcommentVM.CommenterUserID = "00000000-0000-000000000001";
            //Console.WriteLine(newcommentVM.CommenterUserID);
            if (ModelState.IsValid)
            {
                var dto = new UserCommentDTO() { };

                dto.CommentID = newcommentVM.CommentID;
                dto.CommentBody = newcommentVM.CommentBody;
                dto.CommenterUserID = newcommentVM.CommenterUserID;
                dto.CommentedScore = newcommentVM.CommentedScore;
                dto.CommentCreatedAt = newcommentVM.CommentCreatedAt;
                dto.CommentModifiedAt = newcommentVM.CommentModifiedAt;
                dto.CommentDeletedAt = newcommentVM.CommentDeletedAt;
                dto.IsHelpful = newcommentVM.IsHelpful;
                dto.IsHarmful = newcommentVM.IsHarmful;
                var result = await _userCommentsServices.NewComment(dto);

                if (result == null)
                {
                    return NotFound();
                }






                //TODO erist ära kas tegu admini või kasutajaga,admin
                //tagastab admin-comments-index,kasutaja aga vastava filmi juurde
                return RedirectToAction(nameof(Index));
                //return ReDirectToAction("Details","Movies",id)
            }


            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> DetailsAdmin(Guid id)
        {
            var requestedComment = await _userCommentsServices.DetailAsync(id);
            if (requestedComment == null) { return NotFound(); }

            var commentVM = new UserCommentsIndexViewModel();

            commentVM.CommentID = (Guid)requestedComment.CommentID;
            commentVM.CommentBody = requestedComment.CommentBody;
            commentVM.CommenterUserID = requestedComment.CommenterUserID;
            commentVM.CommentedScore = requestedComment.CommentedScore;
            commentVM.CommentCreatedAt = (DateTime)requestedComment.CommentCreatedAt;
            commentVM.CommentDeletedAt = requestedComment.CommentDeletedAt;
            commentVM.CommentModifiedAt = requestedComment.CommentModifiedAt;


            return View(commentVM);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteComment(Guid id)
        {
            var deleteEntry = await _userCommentsServices.DetailAsync(id);
            if (deleteEntry == null) { return NotFound(); }

            var commentVM = new UserCommentsIndexViewModel();
            commentVM.CommentID = deleteEntry.CommentID;
            commentVM.CommentBody = deleteEntry.CommentBody;
            commentVM.CommenterUserID = deleteEntry.CommenterUserID;
            commentVM.CommentedScore = deleteEntry.CommentedScore;
            commentVM.CommentCreatedAt = deleteEntry.CommentCreatedAt;
            commentVM.CommentDeletedAt = deleteEntry.CommentDeletedAt;
            return View("DeleteAdmin",commentVM);



        }
        [HttpPost, ActionName("DeleteCommentAdmin")]
        public async Task<IActionResult> DeleteAdminPost(Guid id)
        {
            var deleteThisComment = await _userCommentsServices.Delete(id);
            if (deleteThisComment == null) { return NotFound(); }
            return View("Index");
         
        }

    }
}
    