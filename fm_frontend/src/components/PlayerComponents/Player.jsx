import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import GeneralAxiosService from "../../services/GeneralAxiosService";
import ErrorMsg from "../ErrorMsg";
import HeaderTabs from "../TabComponents/HeaderTabs";
import Loading from "../Loading";
import PlayerHeader from "./PlayerHeader";
import TabPanel from "../TabComponents/TabPanel";
import PlayerInfo from "./PlayerInfo";
import PlayerStats from "./PlayerStats";
import PlayerRecord from "./PlayerRecord";

const Player = () => {
    const [user] = useState(JSON.parse(localStorage.getItem("User")));
    const navigate = useNavigate();
    const params = useParams();
    const [player, setPlayer] = useState({});
    const [loading, setLoading] = useState(true);
    const [hasError, setHasError] = useState(false);
    const [value, setValue] = useState(0);

    const handleChange = (event, newValue) => {
      setValue(newValue);
    };

    useEffect(() => {
        if(user == null){
            navigate("/");
        }else{
            GeneralAxiosService.getMethod("https://localhost:7067/api/v1/Players/"+params.id)
            .then((response) => setPlayer(response.data))
            .then(() => setLoading(false))
            .catch((e) => {
                setLoading(false);
                setHasError(true);
            });
        }
    },[params.id, user, navigate])

    useEffect(() => {
        if(user == null){
            navigate("/");
        }
    }, [user, navigate]);

    return <section className='playerSection container container--pa'>
        <h1 className="title">Player</h1>
        {loading && <Loading />}
        {hasError && <ErrorMsg />}
        {(!loading && !hasError) && <div className="playerHeader container container--styled">
            <PlayerHeader player={player}/>
            <HeaderTabs 
                tabs={["Info", "Record"]} 
                value={value} 
                handleChange={handleChange}
            />
        </div>}

        {(!loading && !hasError)  && <div className="tabContent">
            <TabPanel value={value} index={0}>
                <div className="tabsGridContainer">
                    <PlayerInfo player={player}/>
                    <PlayerStats player={player}/>
                </div>
            </TabPanel>
            <TabPanel value={value} index={1}>
                <PlayerRecord player={player}/>
            </TabPanel>     
        </div>}
    </section>
}

export default Player;