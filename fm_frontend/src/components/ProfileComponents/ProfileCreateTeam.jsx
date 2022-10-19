import Button from '@mui/material/Button';
import { useState } from 'react';
import GeneralAxiosService from '../../services/GeneralAxiosService';
import CreateTeamForm from '../HomeComponents/CreateTeamForm';

const ProfileCreateTeam = (props) => {
    const [openForm, setOpenForm] = useState(false);
    
    const handleClickOpen = () => {
        setOpenForm(true);
    };
  
    const handleClose = () => {
        setOpenForm(false);
    };

    const handleTeamCreate = (data) => {
        GeneralAxiosService.postMethod("https://localhost:7067/api/v1/Users/"+ props.user.customer.userId +"/CreateTeam", data)
        .then((res) => {
            props.user.customer.hasTeam = true;
            localStorage.setItem("User", JSON.stringify(props.user));
            window.location.reload();
        });
    };

    return <>
        <CreateTeamForm 
            open={openForm}
            handleClose={handleClose}
            handleTeamCreate={handleTeamCreate}
        />

        <div style={{margin: "1rem auto"}} className="profileCreateTeam container container--styled">
             <div>
                <p>It appears that you don't manage a team!</p>   
                <p>Want to create one?</p>
             </div>
             <Button onClick={handleClickOpen} disableElevation color="success" variant="contained">Create Team</Button>
        </div>
    </>
    
}

export default ProfileCreateTeam;