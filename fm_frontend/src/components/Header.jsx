import React from "react";

const Header = () => {
    return <>
        <header>
            <nav className="navbar container DesktopNav flex flex-ai-c flex-jc-sb">
                <div className="navbar__brand">
                    <a className="brand_logo flex flex-ai-c" href="./">
                        <img className="brandImg" src="./Images/logo.png" alt="logo"></img>
                    </a>
                </div>
                <div className="navbar__links">
                    <a href="./">Home</a>
                    <a href="./">Leagues</a>
                    <a href="./">Teams</a>
                    <a href="./">Matches</a>
                </div>
                <div className="navbar__userProfile">
                    <a href="./">
                        <img src="https://pbs.twimg.com/profile_images/1484245584978616324/PyqroykF_400x400.png" alt="profile"></img>
                    </a>
                </div>
            </nav>
            <nav className="navbar MobileNav">
                <div className="navbar__Links flex flex-ai-c flex-jc-sb">
                    <a href="./">Home</a>
                    <a href="./">Leagues</a>
                    <a href="./">Teams</a>
                    <a href="./">Matches</a>
                </div>
            </nav>
        </header>
        
    </>;        
}

export default Header;