import * as React from 'react';
import { Link } from 'react-router-dom';
import Box from '@mui/material/Box';
import BottomNavigation from '@mui/material/BottomNavigation';
import BottomNavigationAction from '@mui/material/BottomNavigationAction';
import SportsSoccerIcon from '@mui/icons-material/SportsSoccer';
import HomeIcon from '@mui/icons-material/Home';
import GroupsIcon from '@mui/icons-material/Groups';
import EmojiEventsIcon from '@mui/icons-material/EmojiEvents';

const Header = () => {
    const [value, setValue] = React.useState(0);

    return <>
        <header>
            <nav className="navbar container DesktopNav flex flex-ai-c flex-jc-sb">
                <div className="navbar__brand">
                    <a className="brand_logo flex flex-ai-c" href="./">
                        <img className="brandImg" src="./Images/logo.png" alt="logo"></img>
                    </a>
                </div>
                <div className="navbar__links">
                    <Link to="/">Home</Link>
                    <Link to="/leagues">Leagues</Link>
                    <Link to="/teams">Teams</Link>
                    <Link to="/matches">Matches</Link>
                </div>
                <div className="navbar__userProfile">
                    <a href="./">
                        <img src="https://pbs.twimg.com/profile_images/1484245584978616324/PyqroykF_400x400.png" alt="profile"></img>
                    </a>
                </div>
            </nav>
            <nav className="navbar MobileNav">
                <Box>
                    <BottomNavigation
                      showLabels
                      value={value}
                      sx = {{backgroundColor: 'inherit'}}
                      onChange={(event, newValue) => {
                        setValue(newValue);
                      }}
                    >
                      <BottomNavigationAction 
                        className="nav-item"
                        component={Link}
                        to="/"
                        label="Home" icon={<HomeIcon sx={{ fontSize: 25 }}/>}
                        />
                      <BottomNavigationAction 
                        component={Link}
                        className="nav-item"
                        to="/leagues"
                        label="Leagues" icon={<EmojiEventsIcon sx={{ fontSize: 25 }} />}/>
                      <BottomNavigationAction 
                        component={Link}
                        className="nav-item"
                        to="/teams"
                        label="Teams" icon={<GroupsIcon sx={{ fontSize: 30 }}/>}/>
                      <BottomNavigationAction 
                        component={Link}
                        className="nav-item"
                        to="/matches"
                        label="Matches" icon={<SportsSoccerIcon sx={{ fontSize: 25 }}/>} />
                    </BottomNavigation>
                </Box>
            </nav>
        </header>
        
    </>;        
}

export default Header;