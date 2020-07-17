﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UcLeftMenu.ascx.cs" Inherits="Usercontrol_LeftMenuAdmin" %>

<div class="rightsidebar span_3_of_1 pull-left">
    <h2>CATEGORIES</h2>
    <ul>
        <li><a href="#">Mobile Phones</a></li>
        <li><a href="#">Desktop</a></li>
        <li><a href="#">Laptop</a></li>
        <li><a href="#">Accessories</a></li>
        <li><a href="#">Software</a></li>
        <li><a href="#">Sports & Fitness</a></li>
        <li><a href="#">Footwear</a></li>
        <li><a href="#">Jewellery</a></li>
        <li><a href="#">Clothing</a></li>
        <li><a href="#">Home Decor & Kitchen</a></li>
        <li><a href="#">Beauty & Healthcare</a></li>
        <li><a href="#">Toys, Kids & Babies</a></li>
    </ul>
    <div class="subscribe">
        <h2>Newsletters Signup</h2>
        <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod.......</p>
        <div class="signup">
            <form>
                <input type="text" value="E-mail address" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = 'E-mail address';"><input type="submit" value="Sign up">
            </form>
        </div>
    </div>
    <div class="community-poll">
        <h2>Community POll</h2>
        <p>What is the main reason for you to purchase products online?</p>
        <div class="poll">
            <form>
                <ul>
                    <li>
                        <input type="radio" name="vote" class="radio" value="1">
                        <span class="label">
                            <label>More convenient shipping and delivery </label>
                        </span>
                    </li>
                    <li>
                        <input type="radio" name="vote" class="radio" value="2">
                        <span class="label">
                            <label for="vote_2">Lower price</label></span>
                    </li>
                    <li>
                        <input type="radio" name="vote" class="radio" value="3">
                        <span class="label">
                            <label for="vote_3">Bigger choice</label></span>
                    </li>
                    <li>
                        <input type="radio" name="vote" class="radio" value="5">
                        <span class="label">
                            <label for="vote_5">Payments security </label>
                        </span>
                    </li>
                    <li>
                        <input type="radio" name="vote" class="radio" value="6">
                        <span class="label">
                            <label for="vote_6">30-day Money Back Guarantee </label>
                        </span>
                    </li>
                    <li>
                        <input type="radio" name="vote" class="radio" value="7">
                        <span class="label">
                            <label for="vote_7">Other.</label></span>
                    </li>
                </ul>
            </form>
        </div>
    </div>
</div>
