import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import GeneralAxiosService from "../../services/GeneralAxiosService";
import Loading from "../Loading";
import HeaderTabs from "../TabComponents/HeaderTabs";
import TabPanel from "../TabComponents/TabPanel";
import LeagueHeader from "./LeagueHeader";
import LeaugeStandings from "./LeagueStandings";
import LeagueStats from "./LeagueStats";

const Leagues = () => {
    const user = JSON.parse(localStorage.getItem("User"));
    const navigate = useNavigate();
    const params = useParams();
    const [loading, setLoading] = useState(true);
    const [league, setLeague] = useState({});
    const [value, setValue] = useState(0);

    const handleChange = (event, newValue) => {
      setValue(newValue);
    };

    useEffect(() => {
        if(user == null){
            navigate("/");
        }else{
            GeneralAxiosService.getMethod('https://localhost:7067/api/v1/Leagues/' + params.id)
            .then((res) => setLeague(res.data))
            .then(() => setLoading(false))
        }
    }, []);

    return <section className='leagueSection container container--pa'>
         <h1 className="title">League Details</h1>
         {loading && <Loading />}
         {!loading && <div className="leaugeHeader container container--styled">
            <LeagueHeader league={league}/>
            <HeaderTabs 
                tabs={["Standings", "Stats"]} 
                value={value} 
                handleChange={handleChange}
            />
         </div>}

         {(!loading)  && <div className="tabContent">
            <TabPanel value={value} index={0}>
                <div className="tabsGridContainer">
                    <LeaugeStandings league={league}/>
                </div>
            </TabPanel>
            <TabPanel value={value} index={1}>
                <LeagueStats league={league} />
            </TabPanel>     
        </div>}
    </section>
}

export default Leagues;