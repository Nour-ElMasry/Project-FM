import { useState, useEffect } from "react";
import GeneralAxiosService from "../../services/GeneralAxiosService";
import Loading from "../Loading";
import PlayersTable from "../PlayerComponents/PlayersTable";

const TeamPlayers = (props) => {
    const team = props.teamId;
    const [players, setPlayers] = useState([]);
    const [loading, setLoading] = useState(true);
    const [page, setPageApi] = useState(1);

    const handlePageChange = (pageNumber) => {
        setPageApi(pageNumber);
    }

    const handleFilterSubmit = (data) => {
      GeneralAxiosService.getMethodWithParams('https://localhost:7067/api/v1/Teams/'+ team +'/Players/' + page, data)
          .then((response) => setPlayers(response.data))
          .then(() => setLoading(false));
    }

    useEffect(() => {
        GeneralAxiosService.getMethod('https://localhost:7067/api/v1/Teams/'+ team +'/Players/' + page)
        .then((response) => setPlayers(response.data))
        .then(() => setLoading(false));
    }, [team, page])

    return <div>
        {loading && <Loading/>}
        {!loading && <PlayersTable playersPage={false} players={players} handlePageChange={handlePageChange} handleFilterSubmit={handleFilterSubmit}/>}
    </div>
}

export default TeamPlayers;