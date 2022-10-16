import { useEffect, useState } from "react";
import GeneralAxiosService from "../../services/GeneralAxiosService";
import LeaugeStandings from "../LeagueComponents/LeagueStandings";
import Loading from "../Loading";
import HomeTeam from "./HomeTeam";
import TeamFixture from "./TeamFixture";

const HomeComponents = (props) => {
    const [team, setTeam] = useState({});
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        GeneralAxiosService.getMethod("https://localhost:7067/api/v1/Users/" + props.user.customer.userId + "/Team")
        .then((res) => setTeam(res.data))
        .then(() => setLoading(false));
    }, [props.user.customer.userId])

    return <div className="HomeComponents">
        {loading && <Loading />}
        {!loading && <>
            <div className="teamAndFixtureContainer">
                <HomeTeam team={team}/>
                <TeamFixture teamId={team.id} />
            </div>
            <LeaugeStandings short title={team.currentLeague.leagueName} leagueId={team.currentLeague.leagueId}/>
        </>}
    </div>
}

export default HomeComponents;