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
    const [isPlayed, setIsPlayed] = useState(false);
    const [nextMatch, setNextMatch] = useState(0);
    const [simulating, setSimulating] = useState(false);

    const simulateMatch = (e) => {
        e.preventDefault();
        setIsPlayed(true);
        setSimulating(true);
        props.simulate();
    }

    const nextGameWeek = (e) => {
        setNextMatch(nextMatch + 1)  
        setIsPlayed(false)  
    }

    useEffect(() => {
        if(!isPlayed){
            GeneralAxiosService.getMethod("https://localhost:7067/Team/"+ props.teamId +"/NextFixture")
            .then((res) => {
                setMatch(res.data)
                setIsPlayed(res.data.isPlayed)
            })
            .then(() => setLoading(false))
            .catch((err) => {
                if(err.response.status === 404)
                    setNotFound(true);

                setLoading(false)
            });
        }else{
            GeneralAxiosService.getMethod("https://localhost:7067/api/v1/Fixtures/" + match.id + "/Result")
            .then((res) => {
                setMatch(res.data)
            })
            .then(() => setSimulating(false))
            .catch((err) => {
                if(err.response.status === 404)
                    setNotFound(true);

                setLoading(false)
            });
        }
    }, [nextMatch, props.refresh]);

    return <div className="TeamFixture container container--styled">
        {loading && <Loading />}
        {(!loading && !isPlayed) && <h3 className="TeamFixtureHeader">Upcoming Fixture</h3>}
        {(!loading && isPlayed) && <>
            {simulating ? <div style={{borderBottom: "1px solid black", padding: "0.5rem 0"}}>
                <p>Macth is being played</p>
            </div>: <div 
                className='nextGameweek'
                onClick={nextGameWeek}>
                    <p>Next Fixture</p>
            </div>}
        </>}
        
        {(!loading && !notFound) && <div className="TeamFixtureInfo">
            {simulating ? <div className="simulateFixtureOverlay">
                <h4>Simulating</h4>
                <div id="wave">
                    <span className="dot"></span>
                    <span className="dot"></span>
                    <span className="dot"></span>
                </div>
            </div> : 
                <Link to={"/matches/" + match.id} className="FixtureOverlay">
                    <h4>View Match Details</h4>
                </Link>
            }
            <MatchItem tableView={true} match={match} />
        </div>}
        {(!loading && !notFound && !isPlayed) && <div 
        className='simulateBtn'
        onClick={simulateMatch}>
           <p>Simulate Fixture</p>
        </div>}
        {notFound && <ErrorMsg />}
    </div>
}

export default TeamFixture;