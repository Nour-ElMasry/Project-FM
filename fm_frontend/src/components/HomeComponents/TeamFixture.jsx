import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import GeneralAxiosService from "../../services/GeneralAxiosService";
import ErrorMsg from "../ErrorMsg";
import Loading from "../Loading";
import MatchItem from "../MatchComponents/MatchItem";

const TeamFixture = (props) => {
    const [match, setMatch] = useState({});
    const [loading, setLoading] = useState(true);
    const [notFound, setNotFound] = useState(false);

    useEffect(() => {
        GeneralAxiosService.getMethod("https://localhost:7067/Team/"+ 1 +"/NextFixture")
        .then((res) => setMatch(res.data))
        .then(() => setLoading(false))
        .catch((err) => {
            if(err.response.status === 404)
                setNotFound(true);
            
            setLoading(false)
        });
    }, []);

    return <div className="TeamFixture container container--styled">
        {loading && <Loading />}
        {!loading && <h3 className="TeamFixtureHeader">Upcoming Fixture</h3>}
        {(!loading && !notFound) && <div className="TeamFixtureInfo">
            <Link to={"/matches/" + match.id} className="FixtureOverlay">
                <h4>View Match Details</h4>
            </Link>
            <MatchItem tableView={true} match={match} />
        </div>}
        {notFound && <ErrorMsg />}
    </div>
}

export default TeamFixture;