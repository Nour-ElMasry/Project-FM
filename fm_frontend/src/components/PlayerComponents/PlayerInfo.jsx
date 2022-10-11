import { Link } from "react-router-dom";
import CountryItem from "../CountryItem";
import Divider from '@mui/material/Divider';
import DateService from "../../services/DateService";

const PlayerInfo = (props) => {
    return <div className="playerMainInfo container--pa container--styled">
        <div className="playerInfoContainer">
            <h2>Player Info</h2>
            <div className="flex flex-jc-c" style={{marginBottom: "1rem"}}>
                <div className="playerMainInfoTeam" >
                    <h3>
                        Team
                    </h3>
                    <Link to={"/teams/"+props.player?.currentTeam?.teamId} className="teamContainer flex flex-ai-c">
                        <span className="teamLogo">
                            <img src={props.player?.currentTeam?.teamLogo} alt="Team"/>
                        </span>
                        <h4>{props.player?.currentTeam?.teamName}</h4>
                    </Link>
                    <br/>
                    <div>
                        <h4>
                            {DateService.dateLongFormat(props.player?.playerPerson?.birthDate)}
                        </h4>
                        <p>Date Of Birth</p>
                    </div>
                </div>
                <Divider className="divider" orientation="vertical" flexItem/>
                <div className="playerMainInfoCountry">
                    <h3>
                        Country
                    </h3>
                    <div className="countryContainer flex flex-ai-c">
                        <span className="countryLogo">
                            <CountryItem country={props.player?.playerPerson?.country}/>
                        </span>
                        <h4>{props.player?.playerPerson?.country}</h4>
                    </div>
                    <br/>
                    <div>
                        <h4>
                            {DateService.getAge(props.player?.playerPerson?.birthDate)}
                        </h4>
                        <p>Age</p>
                    </div>
                </div>
            </div>
        </div>
    </div>
}

export default PlayerInfo;