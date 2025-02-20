﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioApp.Core.DTOs.Web.Comment;
using PortfolioApp.Core.Interfaces;
using PortfolioApp.WebMVC.Models.ViewModels;

namespace PortfolioApp.WebMVC.Controllers;
public class CommentController : Controller
{
    private readonly ICommentService _commentService;
    private readonly IMapper _mapper;
    public CommentController(ICommentService commentService, IMapper mapper)
    {
        _commentService = commentService;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromForm] AddCommentViewModel model)
    {
        if (!ModelState.IsValid)
        {
            if(model.SignedCommenterName is not null)
                TempData["CommentError"] = "Lütfen formu düzgün şekilde doldurunuz. Yorum zorunlu ve en fazla 300 karakter olabilir.";
            else
                TempData["CommentError"] = "Lütfen formu düzgün şekilde doldurunuz. İsim zorunlu ve en fazla 30 , yorum zorunlu ve en fazla 300 karakter olabilir.";

            return Redirect($"/BlogPost/BlogPost/{model.BlogPostId}/#addCommentSection");
        }

        var dto = _mapper.Map<AddCommentDto>(model);

        var result =await _commentService.AddAsync(dto);

        if (result.IsSuccess)
        {
            TempData["Message"] = result.Message;
        }
        else
        {
            TempData["ErrorMessage"] = result.Message;
        }

        return Redirect($"/BlogPost/BlogPost/{model.BlogPostId}/#addCommentSection");
    }

    [Authorize(Roles ="User")]
    [HttpPost]
    public async Task<IActionResult> Delete([FromForm] DeleteCommentViewModel model)
    {
        var referer = Request.Headers["Referer"].ToString();

        if(model.UserId != model.CommenterId)
        {
            TempData["ErrorMessage"] = "Size ait olmayan bir yorumu silmeye çalıştınız!";

            if (!string.IsNullOrEmpty(referer))
            {
                return Redirect(referer);
            }

            return Redirect("/BlogPost/All");
        }

        var result = await _commentService.DeleteAsync(model.CommentId);

        if (result.IsSuccess)
        {
            TempData["Message"] = result.Message;
        }
        else
        {
            TempData["ErrorMessage"] = result.Message;
        }

        if (!string.IsNullOrEmpty(referer))
        {
            return Redirect(referer);
        }

        return Redirect("/BlogPost/All");
    }
}
