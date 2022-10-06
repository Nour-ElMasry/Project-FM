import { useState, useEffect } from "react";
import GeneralAxiosService from "../../services/GeneralAxiosService";
import Loading from "../Loading";
import PlayersTable from "../PlayerComponents/PlayersTable";

const TeamPlayers = (props) => {
    const team = props.teamId;
    const [players, setPlayers] = useState([]);
    const [loading, setLoading] = useState(true);
    const [page, setPageApi] = useState(1);
    const [filterData, setFilterData] = useState({});

    const handlePageChange = (pageNumber) => {
        setPageApi(pageNumber);
    }

    const handleFilterSubmit = (data) => {
        setFilterData(data);
        setPageApi(1);
    }

    useEffect(() => {
        GeneralAxiosService.getMethodWithParams('https://localhost:7067/api/v1/Teams/'+ team +'/Players/' + page, filterData)
        .then((response) => setPlayers(response.data))
        .then(() => setLoading(false));
    }, [team, page, filterData])

    return <div>
        {loading && <Loading/>}
        {!loading && <PlayersTable playersPage={false} players={players} handlePageChange={handlePageChange} handleFilterSubmit={handleFilterSubmit}/>}
    </div>
}

export default TeamPlayers;