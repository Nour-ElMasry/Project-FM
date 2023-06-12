import { useEffect, useState } from "react";
import GeneralAxiosService from "../../services/GeneralAxiosService";
import ErrorMsg from "../ErrorMsg";
import LeaugeStandings from "../LeagueComponents/LeagueStandings";
import Loading from "../Loading";
import HomeTeam from "./HomeTeam";
import TeamFixture from "./TeamFixture";

const HomeComponents = (props) => {
  const [team, setTeam] = useState({});
  const [loading, setLoading] = useState(true);
  const [gameWeek, setGameWeek] = useState(1);
  const [error, setError] = useState(false);

  useEffect(() => {
    GeneralAxiosService.getMethod(
      "https://localhost:7067/api/v1/Users/" +
        props.user.customer.userId +
        "/Team"
    )
      .then((res) => {
        localStorage.setItem("UserTeamId", res.data.id);
        setTeam(res.data);
      })
      .then(() => setLoading(false))
      .catch((err) => {
        setLoading(false);
        setError(true);
        if (err.response.status === 404) {
          props.user.customer.hasTeam = false;
          localStorage.setItem("User", JSON.stringify(props.user));
          window.location.reload();
        }
      });
  }, [props.user.customer.userId]);

  const gameWeekSimulation = () => {
    setGameWeek((prev) => prev + 1);
  };

  return (
    <div className="HomeComponents">
      {loading && !error && <Loading />}
      {!loading && !error && (
        <>
          <div className="teamAndFixtureContainer">
            <HomeTeam team={team} />
            <TeamFixture
              refresh={gameWeek}
              simulate={gameWeekSimulation}
              teamId={team.id}
            />
          </div>
          <LeaugeStandings
            short
            refresh={gameWeek}
            title={team.currentLeague.leagueName}
            leagueId={team.currentLeague.leagueId}
          />
        </>
      )}
      {error && <ErrorMsg />}
    </div>
  );
};

export default HomeComponents;
