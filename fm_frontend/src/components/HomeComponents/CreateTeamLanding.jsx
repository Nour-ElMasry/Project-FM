import Button from '@mui/material/Button';
import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import GeneralAxiosService from '../../services/GeneralAxiosService';
import CreateTeamForm from './CreateTeamForm';

const CreateTeamLanding = () => {
    const [user] = useState(JSON.parse(localStorage.getItem("User")));
    const navigate = useNavigate();
    const [openForm, setOpenForm] = useState(false);
    
    const handleClickOpen = () => {
        setOpenForm(true);
    };
  
    const handleClose = () => {
        setOpenForm(false);
    };

    const handleTeamCreate = (data) => {
        GeneralAxiosService.postMethod("https://localhost:7067/api/v1/Users/"+ user.customer.userId +"/CreateTeam", data)
        .then((res) => {
            user.customer.hasTeam = true;
            localStorage.setItem("User", JSON.stringify(user));
            window.location.reload();
        });
    };

    useEffect(() => {
        if(user == null){
            navigate("/");
        }
    }, [user, navigate]);

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