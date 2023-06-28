import Divider from "@mui/material/Divider";
import ErrorOutlineIcon from "@mui/icons-material/ErrorOutline";

const MatchEvents = (props) => {
  var groupByTeamName = (list) => {
    return list.reduce((a, b, i) => {
      (a[b["playerScorer"].currentTeam.teamName] =
        a[b["playerScorer"].currentTeam.teamName] || []).push({
        event: b,
        order: i,
      });
      return a;
    }, {});
  };

  var homeTeam = props.match.homeTeam.teamName;
  var awayTeam = props.match.awayTeam.teamName;

  var homeScore = 0;
  var awayScore = 0;

  var eventList = [];

  var scoreBoard = [];
  if (props.match.fixtureEvents.length > 0) {
    eventList = groupByTeamName(props.match.fixtureEvents);
    scoreBoard = props.match.fixtureEvents.map((e) => {
      if (e.playerScorer.currentTeam.teamName === homeTeam) {
        homeScore += 1;
      } else {
        awayScore += 1;
      }
      return {
        home: homeScore,
        away: awayScore,
      };
    });
  }

  return (
    <>
      {props.match.fixtureEvents.length > 0 ? (
        <div className="eventsTable flex flex-ai-c">
          <div className="homeTeamEvents teamEvent">
            {homeScore > 0 &&
              eventList[homeTeam]
                .sort((a, b) => b.order - a.order)
                .map((e, i) => {
                  var scorerName = e.event.playerScorer.playerPerson.name;
                  var assisterName =
                    e.event.playerAssister != null
                      ? e.event.playerAssister.playerPerson.name
                      : null;
                  return (
                    <div key={i} className="goal">
                      <p className="score">
                        ({scoreBoard[e.order].home} - {scoreBoard[e.order].away}
                        )
                      </p>
                      <p className="scorer">{scorerName}</p>
                      {assisterName != null ? (
                        <p className="assister">{assisterName}</p>
                      ) : (
                        <></>
                      )}
                    </div>
                  );
                })}
          </div>
          <Divider orientation="vertical" flexItem />
          <div className="awayTeamEvents teamEvent">
            {awayScore > 0 &&
              eventList[awayTeam]
                .sort((a, b) => b.order - a.order)
                .map((e, i) => {
                  var scorerName = e.event.playerScorer.playerPerson.name;
                  var assisterName =
                    e.event.playerAssister != null
                      ? e.event.playerAssister.playerPerson.name
                      : null;
                  return (
                    <div key={i} className="goal">
                      <p className="score">
                        ({scoreBoard[e.order].home} - {scoreBoard[e.order].away}
                        )
                      </p>
                      <p className="scorer">{scorerName}</p>
                      {assisterName != null ? (
                        <p className="assister">{assisterName}</p>
                      ) : (
                        <></>
                      )}
                    </div>
                  );
                })}
          </div>
        </div>
      ) : (
        <div className="eventsTable flex flex-ai-c flex-jc-c">
          <ErrorOutlineIcon className="noEventsErrorIcon"></ErrorOutlineIcon>
          <p className="noEvents">No events took place in this match</p>
        </div>
      )}
    </>
  );
};

export default MatchEvents;
