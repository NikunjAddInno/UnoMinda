//  To parse this JSON data, first install
//
//      Boost     http://www.boost.org
//      json.hpp  https://github.com/nlohmann/json
//
//  Then include this file, and then do
//
//     modelData_Cat data = nlohmann::json::parse(jsonString);

#pragma once

//#include "json.hpp"
#include <nlohmann/json.hpp>
//#include <boost/optional.hpp>
#include <stdexcept>
#include <regex>

namespace toolCpp_cat {
    using nlohmann::json;

    inline json get_untyped(const json & j, const char * property) {
        if (j.find(property) != j.end()) {
            return j.at(property).get<json>();
        }
        return json();
    }

    inline json get_untyped(const json & j, std::string property) {
        return get_untyped(j, property.data());
    }

    class DictThresholds {
        public:
        DictThresholds() = default;
        virtual ~DictThresholds() = default;

        private:
        double edge_threshold;

        public:
        const double & get_edge_threshold() const { return edge_threshold; }
        double & get_mutable_edge_threshold() { return edge_threshold; }
        void set_edge_threshold(const double & value) { this->edge_threshold = value; }
    };

    class ListInsRoi {
        public:
        ListInsRoi() = default;
        virtual ~ListInsRoi() = default;

        private:
        DictThresholds dict_thresholds;
        std::string roi;
        std::vector<std::string> rect_pts;
        bool enabled;
        int64_t idx;
        std::string name;

        public:
        const DictThresholds & get_dict_thresholds() const { return dict_thresholds; }
        DictThresholds & get_mutable_dict_thresholds() { return dict_thresholds; }
        void set_dict_thresholds(const DictThresholds & value) { this->dict_thresholds = value; }

        const std::string & get_roi() const { return roi; }
        std::string & get_mutable_roi() { return roi; }
        void set_roi(const std::string & value) { this->roi = value; }

        const std::vector<std::string> & get_rect_pts() const { return rect_pts; }
        std::vector<std::string> & get_mutable_rect_pts() { return rect_pts; }
        void set_rect_pts(const std::vector<std::string> & value) { this->rect_pts = value; }

        const bool & get_enabled() const { return enabled; }
        bool & get_mutable_enabled() { return enabled; }
        void set_enabled(const bool & value) { this->enabled = value; }

        const int64_t & get_idx() const { return idx; }
        int64_t & get_mutable_idx() { return idx; }
        void set_idx(const int64_t & value) { this->idx = value; }

        const std::string & get_name() const { return name; }
        std::string & get_mutable_name() { return name; }
        void set_name(const std::string & value) { this->name = value; }
    };

    class ListInspection {
        public:
        ListInspection() = default;
        virtual ~ListInspection() = default;

        private:
        std::string par_name;
        bool enabled;
        double std_value;
        double tolerance_l;
        double tolerance_h;
        int64_t result_val;
        bool m_result;

        public:
        const std::string & get_par_name() const { return par_name; }
        std::string & get_mutable_par_name() { return par_name; }
        void set_par_name(const std::string & value) { this->par_name = value; }

        const bool & get_enabled() const { return enabled; }
        bool & get_mutable_enabled() { return enabled; }
        void set_enabled(const bool & value) { this->enabled = value; }

        const double & get_std_value() const { return std_value; }
        double & get_mutable_std_value() { return std_value; }
        void set_std_value(const double & value) { this->std_value = value; }

        const double & get_tolerance_l() const { return tolerance_l; }
        double & get_mutable_tolerance_l() { return tolerance_l; }
        void set_tolerance_l(const double & value) { this->tolerance_l = value; }

        const double & get_tolerance_h() const { return tolerance_h; }
        double & get_mutable_tolerance_h() { return tolerance_h; }
        void set_tolerance_h(const double & value) { this->tolerance_h = value; }

        const int64_t & get_result_val() const { return result_val; }
        int64_t & get_mutable_result_val() { return result_val; }
        void set_result_val(const int64_t & value) { this->result_val = value; }

        const bool & get_m_result() const { return m_result; }
        bool & get_mutable_m_result() { return m_result; }
        void set_m_result(const bool & value) { this->m_result = value; }
    };

    class modelData_Cat {
        public:
        modelData_Cat() = default;
        virtual ~modelData_Cat() = default;

        private:
        std::vector<ListInspection> list_inspections;
        std::vector<ListInsRoi >list_ins_roi;
        int64_t inspection_param_cnt;
        int64_t camera_idx;
        int64_t camexposure;

        public:
        const std::vector<ListInspection> & get_list_inspections() const { return list_inspections; }
        std::vector<ListInspection> & get_mutable_list_inspections() { return list_inspections; }
        void set_list_inspections(const std::vector<ListInspection> & value) { this->list_inspections = value; }

        const std::vector<ListInsRoi> & get_list_ins_roi() const { return list_ins_roi; }
        std::vector<ListInsRoi> & get_mutable_list_ins_roi() { return list_ins_roi; }
        void set_list_ins_roi(const std::vector<ListInsRoi> & value) { this->list_ins_roi = value; }

        const int64_t & get_inspection_param_cnt() const { return inspection_param_cnt; }
        int64_t & get_mutable_inspection_param_cnt() { return inspection_param_cnt; }
        void set_inspection_param_cnt(const int64_t & value) { this->inspection_param_cnt = value; }

        const int64_t & get_camera_idx() const { return camera_idx; }
        int64_t & get_mutable_camera_idx() { return camera_idx; }
        void set_camera_idx(const int64_t & value) { this->camera_idx = value; }

        const int64_t & get_camexposure() const { return camexposure; }
        int64_t & get_mutable_camexposure() { return camexposure; }
        void set_camexposure(const int64_t & value) { this->camexposure = value; }
    };

    void from_json(const json & j, toolCpp_cat::DictThresholds & x);
    void to_json(json & j, const toolCpp_cat::DictThresholds & x);

    void from_json(const json & j, toolCpp_cat::ListInsRoi & x);
    void to_json(json & j, const toolCpp_cat::ListInsRoi & x);

    void from_json(const json & j, toolCpp_cat::ListInspection & x);
    void to_json(json & j, const toolCpp_cat::ListInspection & x);

    void from_json(const json & j, toolCpp_cat::modelData_Cat & x);
    void to_json(json & j, const toolCpp_cat::modelData_Cat & x);

    inline void from_json(const json & j, toolCpp_cat::DictThresholds& x) {
        x.set_edge_threshold(j.at("Edge Threshold").get<double>());
    }

    inline void to_json(json & j, const toolCpp_cat::DictThresholds & x) {
        j = json::object();
        j["Edge Threshold"] = x.get_edge_threshold();
    }

    inline void from_json(const json & j, toolCpp_cat::ListInsRoi& x) {
        x.set_dict_thresholds(j.at("Dict_thresholds").get<toolCpp_cat::DictThresholds>());
        x.set_roi(j.at("Roi").get<std::string>());
        x.set_rect_pts(j.at("Rect_pts").get<std::vector<std::string>>());
        x.set_enabled(j.at("Enabled").get<bool>());
        x.set_idx(j.at("Idx").get<int64_t>());
        x.set_name(j.at("Name").get<std::string>());
    }

    inline void to_json(json & j, const toolCpp_cat::ListInsRoi & x) {
        j = json::object();
        j["Dict_thresholds"] = x.get_dict_thresholds();
        j["Roi"] = x.get_roi();
        j["Rect_pts"] = x.get_rect_pts();
        j["Enabled"] = x.get_enabled();
        j["Idx"] = x.get_idx();
        j["Name"] = x.get_name();
    }

    inline void from_json(const json & j, toolCpp_cat::ListInspection& x) {
        x.set_par_name(j.at("parName").get<std::string>());
        x.set_enabled(j.at("enabled").get<bool>());
        x.set_std_value(j.at("stdValue").get<double>());
        x.set_tolerance_l(j.at("toleranceL").get<double>());
        x.set_tolerance_h(j.at("toleranceH").get<double>());
        x.set_result_val(j.at("resultVal").get<int64_t>());
        x.set_m_result(j.at("m_result").get<bool>());
    }

    inline void to_json(json & j, const toolCpp_cat::ListInspection & x) {
        j = json::object();
        j["parName"] = x.get_par_name();
        j["enabled"] = x.get_enabled();
        j["stdValue"] = x.get_std_value();
        j["toleranceL"] = x.get_tolerance_l();
        j["toleranceH"] = x.get_tolerance_h();
        j["resultVal"] = x.get_result_val();
        j["m_result"] = x.get_m_result();
    }

    inline void from_json(const json & j, toolCpp_cat::modelData_Cat& x) {
        x.set_list_inspections(j.at("List_inspections").get<std::vector<toolCpp_cat::ListInspection>>());
        x.set_list_ins_roi(j.at("List_ins_roi").get<std::vector<toolCpp_cat::ListInsRoi>>());
        x.set_inspection_param_cnt(j.at("InspectionParamCnt").get<int64_t>());
        x.set_camera_idx(j.at("CameraIdx").get<int64_t>());
        x.set_camexposure(j.at("Camexposure").get<int64_t>());
    }

    inline void to_json(json & j, const toolCpp_cat::modelData_Cat & x) {
        j = json::object();
        j["List_inspections"] = x.get_list_inspections();
        j["List_ins_roi"] = x.get_list_ins_roi();
        j["InspectionParamCnt"] = x.get_inspection_param_cnt();
        j["CameraIdx"] = x.get_camera_idx();
        j["Camexposure"] = x.get_camexposure();
    }
}

//namespace nlohmann {

//}
