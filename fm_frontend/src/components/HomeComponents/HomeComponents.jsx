import { useEffect, useState } from "react";
import GeneralAxiosService from "../../services/GeneralAxiosService";
import LeaugeStandings from "../LeagueComponents/LeagueStandings";
import Loading from "../Loading";
import HomeTeam from "./HomeTeam";
import TeamFixture from "./TeamFixture";

const HomeComponents = (props) => {
    const [team, setTeam] = useState({});
    const [loading, setLoading] = useState(true);
    const [gameWeek, setGameWeek] = useState(1);

    useEffect(() => {
        GeneralAxiosService.getMethod("https://localhost:7067/api/v1/Users/" + props.user.customer.userId + "/Team")
        .then((res) => setTeam(res.data))
        .then(() => setLoading(false));
    }, [props.user.customer.userId])

    const gameWeekSimulation = () => {
        GeneralAxiosService.getMethod("https://localhost:7067/api/v1/Fixtures/SimulateGameWeekFixture")
        .then(() => setGameWeek(prev => prev + 1))
    }

    return <div className="HomeComponents">
        {loading && <Loading />}
        {!loading && <>
            <div className="teamAndFixtureContainer">
                <HomeTeam team={team}/>
                <TeamFixture refresh={gameWeek} simulate={gameWeekSimulation} teamId={team.id} />
            </div>
            <LeaugeStandings short refresh={gameWeek} title={team.currentLeague.leagueName} leagueId={team.currentLeague.leagueId}/>
        </>}
    </div>
}

export default HomeComponents;