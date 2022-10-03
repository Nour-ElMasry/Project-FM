import { useState, useEffect } from "react";
import GeneralAxiosService from "../../services/GeneralAxiosService";
import Loading from "../Loading";
import PlayersTable from "../PlayerComponents/PlayersTable";

const getPageNumber = () => {
  if(sessionStorage && parseInt(sessionStorage.getItem("TeamPlayersPage_Key")) > 0) {
    return parseInt(sessionStorage.getItem("TeamPlayersPage_Key"));
  }
  return 1;
}

const TeamPlayers = (props) => {
    const team = props.teamId;
    const [players, setPlayers] = useState([]);
    const [loading, setLoading] = useState(true);
    const [page, setPageApi] = useState(getPageNumber());

    const handlePageChange = (pageNumber) => {
        setPageApi(pageNumber);
        sessionStorage.setItem("TeamPlayersPage_Key", pageNumber)
    }

    useEffect(() => {
        GeneralAxiosService.getMethod('https://localhost:7067/api/v1/Teams/'+ team +'/Players/' + page)
        .then((response) => setPlayers(response.data))
        .then(() => setLoading(false));
    }, [team, page])

    return <div>
        {loading && <Loading/>}
        {!loading && <PlayersTable playersPage={false} players={players} handlePageChange={handlePageChange}/>}
    </div>
}

export default TeamPlayers;