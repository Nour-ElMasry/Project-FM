import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import GeneralAxiosService from "../../services/GeneralAxiosService";
import ErrorMsg from "../ErrorMsg";
import Loading from "../Loading";
import MatchItem from "../MatchComponents/MatchItem";
import EndSeason from "./EndSeason";

const TeamFixture = (props) => {
    const [match, setMatch] = useState({});
    const [loading, setLoading] = useState(true);
    const [notFound, setNotFound] = useState(false);
    const [isPlayed, setIsPlayed] = useState(false);
    const [nextMatch, setNextMatch] = useState(0);
    const [simulating, setSimulating] = useState(false);
    const [endSeason, setEndSeason] = useState(false);

    const simulateMatch = (e) => {
        e.preventDefault();
        setIsPlayed(true);
        setSimulating(true);
        GeneralAxiosService.getMethod("https://localhost:7067/api/v1/Fixtures/SimulateGameWeekFixture")
        .then(() => props.simulate())
        .catch((err) => {
            setSimulating(false);
            setNotFound(true);
        })

    }

    const nextGameWeek = (e) => {
        setNextMatch(nextMatch + 1)  
        setIsPlayed(false)  
    }

    const endSeasonCheck = () => {
        var flag = true;
        GeneralAxiosService.getMethod("https://localhost:7067/api/v1/Leagues/CheckEndSeason")
        .then((res) => flag = res.data)

        return flag;
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
                if(err.response.status === 404){
                    if(endSeasonCheck()){
                        setEndSeason(true);
                    }else{
                        setNotFound(true);
                    }
                }

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
        {(!loading && !isPlayed && !endSeason) && <h3 className="TeamFixtureHeader">Upcoming Fixture</h3>}
        {(!loading && isPlayed) && <>
            {simulating ? <div style={{borderBottom: "1px solid black", padding: "0.5rem 0"}}>
                <p>Match is being played</p>
            </div>: <div 
                className='nextGameweek'
                onClick={nextGameWeek}>
                    <p>Next Fixture</p>
            </div>}
        </>}
        
        {(!loading && !notFound && !endSeason) && <div className="TeamFixtureInfo">
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
        {(!loading && !notFound && !isPlayed && !endSeason) && <div 
        className='simulateBtn'
        onClick={simulateMatch}>
           <p>Simulate Fixture</p>
        </div>}
        {notFound && <ErrorMsg />}

        {endSeason && <h3 className="TeamFixtureHeader">Season Ended</h3>}
        {endSeason && <EndSeason />}
    </div>
}

export default TeamFixture;