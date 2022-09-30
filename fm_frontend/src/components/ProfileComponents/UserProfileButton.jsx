import { useState } from 'react';
import Box from '@mui/material/Box';
import MenuItem from '@mui/material/MenuItem';
import Menu from '@mui/material/Menu';
import Avatar from '@mui/material/Avatar';
import IconButton from '@mui/material/IconButton';
import { Link } from 'react-router-dom';

const UserProfileButton = () => {
    const [anchorEl, setAnchorEl] = useState(null);

    const handleOpenUserMenu = (event) => {
      setAnchorEl(event.currentTarget);
    };

    const handleClose = () => {
      setAnchorEl(null);
    };

    return <Box>
    <IconButton onClick={handleOpenUserMenu} sx={{ p: 0 }}>
        <Avatar alt="Remy Sharp" src="https://pbs.twimg.com/profile_images/1484245584978616324/PyqroykF_400x400.png" />
    </IconButton>

    <Menu
      sx={{ mt: '45px' }}
      id="menu-appbar"
      anchorEl={anchorEl}
      anchorOrigin={{
        vertical: 'top',
        horizontal: 'right',
      }}
      keepMounted
      transformOrigin={{
        vertical: 'top',
        horizontal: 'right',
      }}
      open={Boolean(anchorEl)}
      onClose={handleClose}
    >
        <MenuItem onClick={handleClose}>
            <Link to="/profile" style={{color: "black"}}>
              Profile
            </Link>
        </MenuItem>

        <MenuItem onClick={handleClose}>
            <Link to="/login" style={{color: "black"}} onClick={() => window.localStorage.clear()}>
              Log Out
            </Link>
        </MenuItem>
    </Menu>
  </Box>
}

export default UserProfileButton;