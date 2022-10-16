import { Link } from "react-router-dom";
import ArrowForwardIosIcon from '@mui/icons-material/ArrowForwardIos';

const HomeTeam = (props) => {
    const team = props.team;

    return <Link 
    to={"/Teams/"+ team.id}
    className="HomeTeam container container--styled flex flex-ai-c flex-jc-sb">
        <div className="flex flex-ai-c">

        <img src={team.logo} alt="user team" />
            <div>
                <p>Team</p>
                <h3>{team.name}</h3>
            </div>
        </div>
        <ArrowForwardIosIcon fontSize="small"/>
    </Link>
}

export default HomeTeam;