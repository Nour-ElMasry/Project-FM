import GeneralAxiosService from "../../services/GeneralAxiosService";
import Button from '@mui/material/Button';
import { useState } from "react";

const EndSeason = () => {
    const [nextSeason, setNextSeason] = useState(false);

    const NextSeason = () => {
        setNextSeason(true);
        GeneralAxiosService.getMethod("https://localhost:7067/api/v1/Leagues/NextSeason")
        .then(() => {
            window.location.reload()
            setNextSeason(false);
        });
    }

    return <div className="endSeasonContainer">
        <h4 style={{marginBottom: "0.5rem"}}>You have reached the end of this season</h4>
        
        {nextSeason && <div className="nextSeasonOverlay">
            <h4>Preparing Next Season</h4>
            <div id="wave">
                <span className="dot"></span>
                <span className="dot"></span>
                <span className="dot"></span>
            </div>
        </div>}

        <Button 
        variant="contained" disableElevation color='success' size="small"
        onClick={NextSeason}>
           <p>Start Next Season</p>
        </Button>
    </div>
}

export default EndSeason;