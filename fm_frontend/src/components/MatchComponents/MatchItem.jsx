import React from 'react';

const MatchItem = (props) => {
    return <>
        <div>
            <img src={props.match.homeTeam.teamLogo} alt='teamLogo'></img>
            <p>{props.match.homeTeam.teamName}</p>
        </div>

        {props.match.isPlayed === true ? 
        <h3>{props.match.fixtureScore.homeScore} - {props.match.fixtureScore.awayScore}</h3> 
        : <h3>vs</h3>}

        <div>
            <img src={props.match.awayTeam.teamLogo} alt='teamLogo'></img>
            <p>{props.match.awayTeam.teamName}</p>
        </div>
    </>
}

export default MatchItem;