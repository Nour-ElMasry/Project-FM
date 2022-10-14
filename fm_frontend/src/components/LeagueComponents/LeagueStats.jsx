import { useEffect, useState } from "react";
import GeneralAxiosService from "../../services/GeneralAxiosService";
import Loading from "../Loading";

import PlayerStatsTable from "./PlayerStatsTable";

const LeagueStats = (props) => {
    const [players, setPlayers] = useState({})
    const [loading, setLoading] = useState(true);

    const [url, setUrl] = useState("Scorers")

    const handleChange = (event) => {
        setUrl(event.target.value);
    };
  
    useEffect(() => {
        GeneralAxiosService.getMethod("https://localhost:7067/api/v1/Leagues/"+ props.league.id +"/Players/" + url)
        .then((res) => setPlayers(res.data))
        .then(() => setLoading(false))
     }, [url]);

    return <>
        {loading && <Loading />}
        {!loading && <PlayerStatsTable url={url} handleChange={handleChange} type={url} players={players}/>}
    </>
}

export default LeagueStats;