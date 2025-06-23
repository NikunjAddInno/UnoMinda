//  To parse this JSON data, first install
//
//      Boost     http://www.boost.org
//      json.hpp  https://github.com/nlohmann/json
//
//  Then include this file, and then do
//
//     ResultFrontCam data = nlohmann::json::parse(jsonString);

#pragma once

#include "json.hpp"

#include <boost/optional.hpp>
#include <stdexcept>
#include <regex>

namespace resultFrontCam {
    using nlohmann::json;

#ifndef NLOHMANN_UNTYPED_resultFrontCam_HELPER
#define NLOHMANN_UNTYPED_resultFrontCam_HELPER
    inline json get_untyped(const json& j, const char* property) {
        if (j.find(property) != j.end()) {
            return j.at(property).get<json>();
        }
        return json();
    }

    inline json get_untyped(const json& j, std::string property) {
        return get_untyped(j, property.data());
    }
#endif

    class Roi {
    public:
        Roi() = default;
        virtual ~Roi() = default;

    private:
        int64_t x;
        int64_t y;
        int64_t width;
        int64_t height;

    public:
        const int64_t& get_x() const { return x; }
        int64_t& get_mutable_x() { return x; }
        void set_x(const int64_t& value) { this->x = value; }

        const int64_t& get_y() const { return y; }
        int64_t& get_mutable_y() { return y; }
        void set_y(const int64_t& value) { this->y = value; }

        const int64_t& get_width() const { return width; }
        int64_t& get_mutable_width() { return width; }
        void set_width(const int64_t& value) { this->width = value; }

        const int64_t& get_height() const { return height; }
        int64_t& get_mutable_height() { return height; }
        void set_height(const int64_t& value) { this->height = value; }

        const cv::Rect getCVrect()
        {
            return Rect(x, y, width, height);
        }

        void setCVrect(Rect r)
        {
            x = r.x;
            y = r.y;
            width = r.width;
            height = r.height;
        }
    };

    class ListDefectDetail {
    public:
        ListDefectDetail() = default;
        virtual ~ListDefectDetail() = default;

    private:
        std::string defect_name;
        Roi cv_rect;
        bool result;

    public:
        const std::string& get_defect_name() const { return defect_name; }
        std::string& get_mutable_defect_name() { return defect_name; }
        void set_defect_name(const std::string& value) { this->defect_name = value; }

        const Roi& get_cv_rect() const { return cv_rect; }
        Roi& get_mutable_cv_rect() { return cv_rect; }
        void set_cv_rect(const Roi& value) { this->cv_rect = value; }

        const bool& get_result() const { return result; }
        bool& get_mutable_result() { return result; }
        void set_result(const bool& value) { this->result = value; }
    };

    class ResultFrontCam {
    public:
        ResultFrontCam() = default;
        virtual ~ResultFrontCam() = default;

    private:
        int64_t pose_num;
        Roi roi;
        std::vector<ListDefectDetail> list_defect_details;
        bool final_result;
        bool is_ro_ionly;

    public:
        const int64_t& get_pose_num() const { return pose_num; }
        int64_t& get_mutable_pose_num() { return pose_num; }
        void set_pose_num(const int64_t& value) { this->pose_num = value; }

        const Roi& get_roi() const { return roi; }
        Roi& get_mutable_roi() { return roi; }
        void set_roi(const Roi& value) { this->roi = value; }

        const std::vector<ListDefectDetail>& get_list_defect_details() const { return list_defect_details; }
        std::vector<ListDefectDetail>& get_mutable_list_defect_details() { return list_defect_details; }
        void set_list_defect_details(const std::vector<ListDefectDetail>& value) { this->list_defect_details = value; }

        const bool& get_final_result() const { return final_result; }
        bool& get_mutable_final_result() { return final_result; }
        void set_final_result(const bool& value) { this->final_result = value; }

        const bool& get_is_ro_ionly() const { return is_ro_ionly; }
        bool& get_mutable_is_ro_ionly() { return is_ro_ionly; }
        void set_is_ro_ionly(const bool& value) { this->is_ro_ionly = value; }
    };

    void from_json(const json& j, Roi& x);
    void to_json(json& j, const Roi& x);

    void from_json(const json& j, ListDefectDetail& x);
    void to_json(json& j, const ListDefectDetail& x);

    void from_json(const json& j, ResultFrontCam& x);
    void to_json(json& j, const ResultFrontCam& x);

    inline void from_json(const json& j, Roi& x) {
        x.set_x(j.at("X").get<int64_t>());
        x.set_y(j.at("Y").get<int64_t>());
        x.set_width(j.at("Width").get<int64_t>());
        x.set_height(j.at("Height").get<int64_t>());
    }

    inline void to_json(json& j, const Roi& x) {
        j = json::object();
        j["X"] = x.get_x();
        j["Y"] = x.get_y();
        j["Width"] = x.get_width();
        j["Height"] = x.get_height();
    }

    inline void from_json(const json& j, ListDefectDetail& x) {
        x.set_defect_name(j.at("DefectName").get<std::string>());
        x.set_cv_rect(j.at("CvRect").get<Roi>());
        x.set_result(j.at("Result").get<bool>());
    }

    inline void to_json(json& j, const ListDefectDetail& x) {
        j = json::object();
        j["DefectName"] = x.get_defect_name();
        j["CvRect"] = x.get_cv_rect();
        j["Result"] = x.get_result();
    }

    inline void from_json(const json& j, ResultFrontCam& x) {
        x.set_pose_num(j.at("poseNum").get<int64_t>());
        x.set_roi(j.at("ROI").get<Roi>());
        x.set_list_defect_details(j.at("list_defectDetails").get<std::vector<ListDefectDetail>>());
        x.set_final_result(j.at("finalResult").get<bool>());
        x.set_is_ro_ionly(j.at("isROIonly").get<bool>());
    }

    inline void to_json(json& j, const ResultFrontCam& x) {
        j = json::object();
        j["poseNum"] = x.get_pose_num();
        j["ROI"] = x.get_roi();
        j["list_defectDetails"] = x.get_list_defect_details();
        j["finalResult"] = x.get_final_result();
        j["isROIonly"] = x.get_is_ro_ionly();
    }
}
