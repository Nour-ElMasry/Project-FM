import Button from '@mui/material/Button';

const ProfileCreateTeam = () => {
    return <div style={{margin: "1rem auto"}} className="profileCreateTeam container container--styled">
         <div>
            <p>It appears that you don't manage a team!</p>   
            <p>Want to create one?</p>
         </div>
         <Button disableElevation color="success" variant="contained">Create Team</Button>
    </div>
}

export default ProfileCreateTeam;