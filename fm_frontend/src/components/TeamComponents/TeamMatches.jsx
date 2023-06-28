import { useEffect, useState } from "react";
import GeneralAxiosService from "../../services/GeneralAxiosService";
import Loading from "../Loading";
import MatchesTable from "../MatchComponents/MatchesTable";

const TeamMatches = (props) => {
  const team = props.teamId;
  const [loading, setLoading] = useState(true);
  const [homeMatches, setHomeMatches] = useState([]);
  const [awayMatches, setAwayMatches] = useState([]);

  useEffect(() => {
    GeneralAxiosService.getMethod(
      "https://localhost:7067/api/v1/Teams/" + team + "/Fixtures/0"
    )
      .then((response) => {
        var matches = response.data;
        setHomeMatches(matches.filter((m) => m.homeTeam.teamId === team));
        setAwayMatches(matches.filter((m) => m.awayTeam.teamId === team));
      })
      .then(() => setLoading(false));
  }, [props.played, team]);

  return (
    <div className="matchSection">
      {!loading && (
        <div className="gameweekTables">
          {homeMatches.length > 0 && (
            <MatchesTable tableTitle="Home" fixtures={homeMatches} />
          )}
          {awayMatches.length > 0 && (
            <MatchesTable tableTitle="Away" fixtures={awayMatches} />
          )}
        </div>
      )}
      {loading && <Loading />}
      {!loading && homeMatches.length <= 0 && awayMatches.length <= 0 && (
        <div className="emptyMsg container--styled">
          <p>No matches found</p>
        </div>
      )}
    </div>
  );
};

export default TeamMatches;
