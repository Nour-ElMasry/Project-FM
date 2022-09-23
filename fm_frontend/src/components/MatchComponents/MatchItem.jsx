import React from 'react';
import { Link } from 'react-router-dom';

const MatchItem = (props) => {
    return <>
        <div>
            {props.tableView ? <>
                <img src={props.match.homeTeam.teamLogo} alt='teamLogo'></img>
                <h4>{props.match.homeTeam.teamName}</h4></> 
                : 
                <Link to={"/teams/"+props.match.homeTeam.teamId}>
                    <img src={props.match.homeTeam.teamLogo} alt='teamLogo'></img>
                    <h4>{props.match.homeTeam.teamName}</h4>
                </Link>
            }
        </div>

        {props.match.isPlayed === true ? 
        <div>
            <h3>{props.match.fixtureScore.homeScore} - {props.match.fixtureScore.awayScore}</h3> 
            <p style={{fontWeight: 'bold'}}>Full time</p> 
        </div>
        : <h3>vs</h3>}

        <div>
        {props.tableView ? <>
                <img src={props.match.awayTeam.teamLogo} alt='teamLogo'></img>
                <h4>{props.match.awayTeam.teamName}</h4></> 
                : 
                <Link to={"/teams/"+props.match.awayTeam.teamId}>
                    <img src={props.match.awayTeam.teamLogo} alt='teamLogo'></img>
                    <h4>{props.match.awayTeam.teamName}</h4>
                </Link>
            }
        </div>
    </>
}

export default MatchItem;