import Button from '@mui/material/Button';

const CreateTeamLanding = () => {
    return <div className="createTeamLanding container container--styled">
        <div className="createTeamLandingPic"></div>
        <div className="createTeamLandingInfo">
            <h3>Start your managing journey!</h3>
            <p>Create your dream team and compete against opponents from the top leagues!</p>
            <Button disableElevation className='createBtn' color="success" variant="contained">Create Team</Button>
        </div>  
    </div>
}

export default CreateTeamLanding;