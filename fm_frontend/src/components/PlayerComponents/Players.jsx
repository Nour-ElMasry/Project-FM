import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import GeneralAxiosService from "../../services/GeneralAxiosService";
import Loading from "../Loading";
import PlayersTable from "./PlayersTable";

const getPageNumber = () => {
    if(sessionStorage && parseInt(sessionStorage.getItem("PlayersPage_Key")) > 0) {
      return parseInt(sessionStorage.getItem("PlayersPage_Key"));
    }
    return 1;
}

const Players = () => {
    const [players, setPlayers] = useState([]);
    const [loading, setLoading] = useState(true);
    const [page, setPageApi] = useState(getPageNumber());
    const [user] = useState(JSON.parse(localStorage.getItem("User")));
    const navigate = useNavigate();

    const handlePageChange = (pageNumber) => {
        setPageApi(pageNumber);
        sessionStorage.setItem("PlayersPage_Key", pageNumber)
    }

    const handleFilterSubmit = (data) => {
        GeneralAxiosService.getMethodWithParams('https://localhost:7067/api/v1/Players/All/' + page, data)
            .then((response) => setPlayers(response.data))
            .then(() => setLoading(false));
    }

    useEffect(() => {
        if(user == null){
            navigate("/login");
        }else{
            GeneralAxiosService.getMethod('https://localhost:7067/api/v1/Players/All/' + page)
            .then((response) => setPlayers(response.data))
            .then(() => setLoading(false));
        }
    }, [user, navigate, page]);

    return <section className='playersSection container container--pa'>
        <h1 className="title">Players Page</h1>
        {loading && <Loading/>}
        {!loading && <PlayersTable handleFilterSubmit={handleFilterSubmit} playersPage={true} loading={loading} players={players} handlePageChange={handlePageChange}/>}
    </section>
}

export default Players;