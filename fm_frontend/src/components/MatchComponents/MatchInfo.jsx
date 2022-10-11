import ScheduleIcon from '@mui/icons-material/Schedule';
import LocationOnIcon from '@mui/icons-material/LocationOn';
import EmojiEventsIcon from '@mui/icons-material/EmojiEvents';
import { Link } from 'react-router-dom';

const MatchInfo = (props) => {
    var date = new Date(props.match.date);
    var venue = props.match.venue;
    var league = props.match.fixtureLeague;
    
    return <>
        <h4>
            Match Info
        </h4>
        <div className='flex flex-ai-c hoverableIcon infoContainer'>
            <div className='infoIcon'>
                <Link to={"/leagues/"+league.leagueId}>
                    <EmojiEventsIcon/>
                </Link>
            </div>
            <div className='infoText'>
                <p className='infoRowTitle'>Competition</p>
                <p className='infoRow'>{league.leagueName}</p>
            </div>
        </div>
        <div className='flex flex-ai-c matchDate infoContainer'>
            <div className='infoIcon'>
                <ScheduleIcon/>
            </div>
            <div className='infoText'>
                <p className='infoRowTitle'>KickOff</p>
                <p className='infoRow'>{date.getDate()}/{date.getMonth()+1}/{date.getFullYear()}</p>
            </div>
        </div>
        <div className='flex flex-ai-c  hoverableIcon infoContainer'>
            <div className='infoIcon'>
                <a href={"http://www.google.com/search?q="+venue+" Stadium"}>
                    <LocationOnIcon/>
                </a>
            </div>
            <div className='infoText'>
                <p className='infoRowTitle'>Stadium</p>
                <p className='infoRow'>{venue}</p>
            </div>
        </div>
    </>
}

export default MatchInfo;
