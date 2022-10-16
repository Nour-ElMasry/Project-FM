import Button from '@mui/material/Button';
import { useState } from 'react';
import GeneralAxiosService from '../../services/GeneralAxiosService';
import CreateTeamForm from './CreateTeamForm';

const CreateTeamLanding = (props) => {
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

        <div className="createTeamLanding container container--styled">
        <div className="createTeamLandingPic"></div>
        <div className="createTeamLandingInfo">
            <h3>Start your managing journey!</h3>
            <p>Create your dream team and compete against opponents from the top leagues!</p>
            <Button 
                onClick={handleClickOpen}
                disableElevation 
                className='createBtn' color="success" variant="contained">
                    Create Team
            </Button>
        </div>  
    </div>
    </>
}

export default CreateTeamLanding;