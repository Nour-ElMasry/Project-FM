import { useState } from 'react';
import { Link } from 'react-router-dom';
import Box from '@mui/material/Box';
import BottomNavigation from '@mui/material/BottomNavigation';
import BottomNavigationAction from '@mui/material/BottomNavigationAction';
import SportsSoccerIcon from '@mui/icons-material/SportsSoccer';
import HomeIcon from '@mui/icons-material/Home';
import GroupsIcon from '@mui/icons-material/Groups';
import UserProfileButton from '../ProfileComponents/UserProfileButton';

const NormalHeader = () => {
    const [value, setValue] = useState(0);
    const [user] = useState(JSON.parse(localStorage.getItem("User")));

    return <>
        <header>
            <nav className="navbar container DesktopNav flex flex-ai-c flex-jc-sb">
                <div className="navbar__brand">
                    <Link className="brand_logo flex flex-ai-c" to="/">
                        <img className="brandImg" src="/Images/logo.png" alt="logo"></img>
                    </Link>
                </div>


                <div className="navbar__links">
                    <Link to="/home">Home</Link>
                      {!user.customer.hasTeam && <>
                        <Link to="/players" onClick={() => {
                        sessionStorage.setItem("PlayersPage_Key", 1)
                      }}>Players</Link>
                      <Link to="/matches" onClick={() => sessionStorage.setItem("Page_Key", 1)}>Matches</Link>
                      </>}
                    </div>

                <div className="navbar__userProfile">
                  <UserProfileButton userImg={user.customer.userPerson.image}/>
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
                        to="/home"
                        label="Home" icon={<HomeIcon sx={{ fontSize: 25 }}/>}
                        />
                      {!user.customer.hasTeam && 
                        <BottomNavigationAction 
                        component={Link}
                        className="nav-item"
                        to="/players"
                        label="Players" icon={<GroupsIcon sx={{ fontSize: 30 }}
                        onClick={() => {
                          sessionStorage.setItem("PlayersPage_Key", 1)
                          sessionStorage.setItem("PlayersFilter_Key", JSON.stringify({
                            name: " ",
                            country: " ",
                            minDateOfBirth: 0,
                            maxDateOfBirth: 0,
                            position: " ",
                            team: 0
                          }))
                        }}
                        />}/>
                      }
                      {!user.customer.hasTeam && <BottomNavigationAction 
                        component={Link}
                        className="nav-item"
                        to="/matches"
                        label="Matches" icon={<SportsSoccerIcon sx={{ fontSize: 25 }}
                        onClick={() => sessionStorage.setItem("Page_Key", 1)}
                        />} />
                      }
                    </BottomNavigation>
                </Box>
            </nav>
        </header>
        
    </>;        
}

export default NormalHeader;