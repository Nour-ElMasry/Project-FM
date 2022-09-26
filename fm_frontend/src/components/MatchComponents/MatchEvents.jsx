import Divider from '@mui/material/Divider';

const MatchEvents = (props) => {
    var groupByTeamName = (list) => {
        return list.reduce((a, b, i) => {
          (a[b['playerScorer'].currentTeam.teamName] = a[b['playerScorer'].currentTeam.teamName] || []).push({
            event: b,
            order: i
          });
          return a;
        }, {});
    };    

    var homeTeam = props.match.homeTeam.teamName;
    var awayTeam = props.match.awayTeam.teamName;

    var homeScore = 0;
    var awayScore = 0;

    var eventList = groupByTeamName(props.match.fixtureEvents);

    var scoreBoard = props.match.fixtureEvents.map((e) => {
        if(e.playerScorer.currentTeam.teamName === homeTeam){
            homeScore+=1;
        }else{
            awayScore+=1;
        }
        return {
            home: homeScore,
            away: awayScore
        }
    })

    return <div className='eventsTable flex flex-ai-c'>
        <div className='homeTeamEvents teamEvent'>
            {
                eventList[homeTeam].sort((a, b) => b.order - a.order).map((e , i) => {
                    var scorerName = e.event.playerScorer.playerPerson.name;
                    var assisterName = e.event.playerAssister != null ? e.event.playerAssister.playerPerson.name : null;
                    return <div key={i} className="goal">
                        <p className='score'>({scoreBoard[e.order].home} - {scoreBoard[e.order].away})</p>
                        <p className='scorer'>{scorerName}</p>
                       {assisterName != null ?  <p className='assister'>{assisterName}</p> : <></>}
                    </div>
                })
            }
        </div>
        <Divider orientation="vertical" flexItem/>
        <div className='awayTeamEvents teamEvent'>
            {
                eventList[awayTeam].sort((a, b) => b.order - a.order).map((e , i) => {
                    var scorerName = e.event.playerScorer.playerPerson.name;
                    var assisterName = e.event.playerAssister != null ? e.event.playerAssister.playerPerson.name : null;
                    return <div key={i} className="goal">
                        <p className='score'>({scoreBoard[e.order].home} - {scoreBoard[e.order].away})</p>
                        <p className='scorer'>{scorerName}</p>
                       {assisterName != null ?  <p className='assister'>{assisterName}</p> : <></>}
                    </div>
                })
            }</div>
    </div>
}

export default MatchEvents;