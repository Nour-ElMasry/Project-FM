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
    const [hasError, setHasError] = useState(false);
    const [page, setPageApi] = useState(getPageNumber());
    const [user] = useState(JSON.parse(localStorage.getItem("User")));
    const [filterData, setFilterData] = useState({});
    const navigate = useNavigate();

    const handlePageChange = (pageNumber) => {
        setPageApi(pageNumber);
        sessionStorage.setItem("PlayersPage_Key", pageNumber)
    }

    const handleFilterSubmit = (data) => {
        setFilterData(data);
        setPageApi(1);
        sessionStorage.setItem("PlayersPage_Key", 1)
    }

    const resetFilterData = () => {
        setFilterData({});
        setPageApi(1);
    }

    useEffect(() => {
        if(user == null){
            navigate("/");
        }else{
            GeneralAxiosService.getMethodWithParams('https://localhost:7067/api/v1/Players/All/' + page, filterData)
            .then((response) => setPlayers(response.data))
            .then(() => {
                setLoading(false);
            })
            .catch((e) => {
                setLoading(false);
                setHasError(true);
            });
        }
    }, [user, navigate, page, filterData]);

    return <section className='playersSection container container--pa'>
        <h1 className="title">Players Page</h1>
        {loading && <Loading/>}
        {(hasError && !loading) && <h2 className='errorMsg'>Oops! Something went wrong!</h2>}
        {(!loading && !hasError) && <PlayersTable resetFilterData={resetFilterData} team={true} handleFilterSubmit={handleFilterSubmit} playersPage={true} loading={loading} players={players} handlePageChange={handlePageChange}/>}
    </section>
}

export default Players;