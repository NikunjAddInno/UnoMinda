//  To parse this JSON data, first install
//
//      Boost     http://www.boost.org
//      json.hpp  https://github.com/nlohmann/json
//
//  Then include this file, and then do
//
//     MarkInspectionTools data = nlohmann::json::parse(jsonString);

#pragma once




#include <boost/optional.hpp>
#include "json.hpp"
#include <stdexcept>
#include <regex>

#ifndef NLOHMANN_OPT_HELPER
#define NLOHMANN_OPT_HELPER
namespace nlohmann {
    template <typename T>
    struct adl_serializer<std::shared_ptr<T>> {
        static void to_json(json& j, const std::shared_ptr<T>& opt) {
            if (!opt) j = nullptr; else j = *opt;
        }

        static std::shared_ptr<T> from_json(const json& j) {
            if (j.is_null()) return std::make_shared<T>(); else return std::make_shared<T>(j.get<T>());
        }
    };
    template <typename T>
    struct adl_serializer<boost::optional<T>> {
        static void to_json(json& j, const boost::optional<T>& opt) {
            if (!opt) j = nullptr; else j = *opt;
        }

        static boost::optional<T> from_json(const json& j) {
            if (j.is_null()) return boost::optional<T>(); else return boost::optional<T>(j.get<T>());
        }
    };
}
#endif

namespace markInspection_tools {
    using nlohmann::json;

#ifndef NLOHMANN_UNTYPED_markInspection_tools_HELPER
#define NLOHMANN_UNTYPED_markInspection_tools_HELPER
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

#ifndef NLOHMANN_OPTIONAL_markInspection_tools_HELPER
#define NLOHMANN_OPTIONAL_markInspection_tools_HELPER
    template <typename T>
    inline std::shared_ptr<T> get_heap_optional(const json& j, const char* property) {
        auto it = j.find(property);
        if (it != j.end() && !it->is_null()) {
            return j.at(property).get<std::shared_ptr<T>>();
        }
        return std::shared_ptr<T>();
    }

    template <typename T>
    inline std::shared_ptr<T> get_heap_optional(const json& j, std::string property) {
        return get_heap_optional<T>(j, property.data());
    }
    template <typename T>
    inline boost::optional<T> get_stack_optional(const json& j, const char* property) {
        auto it = j.find(property);
        if (it != j.end() && !it->is_null()) {
            return j.at(property).get<boost::optional<T>>();
        }
        return boost::optional<T>();
    }

    template <typename T>
    inline boost::optional<T> get_stack_optional(const json& j, std::string property) {
        return get_stack_optional<T>(j, property.data());
    }
#endif

    class FullRoi {
    public:
        FullRoi() = default;
        virtual ~FullRoi() = default;

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
       
        //mod
        const Rect getCVrect()
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

    class CenterLoc {
    public:
        CenterLoc() = default;
        virtual ~CenterLoc() = default;

    private:
        double x;
        double y;

    public:
        const double& get_x() const { return x; }
        double& get_mutable_x() { return x; }
        void set_x(const double& value) { this->x = value; }

        const double& get_y() const { return y; }
        double& get_mutable_y() { return y; }
        void set_y(const double& value) { this->y = value; }


        //mod
        const Point2f getCVpoint()
        {
            return Point2f(x, y);
        }

        void setCVpoint(Point2f pt)
        {
            x = pt.x;
            y = pt.y;
        }
    };

    class ObjToolShape {
    public:
        ObjToolShape() = default;
        virtual ~ObjToolShape() = default;

    private:
        int64_t idx;
        nlohmann::json name;
        int64_t shape;
        std::vector<CenterLoc> list_points;
        int64_t point_count;
        bool points_available;
        bool data_saved;

    public:
        const int64_t& get_idx() const { return idx; }
        int64_t& get_mutable_idx() { return idx; }
        void set_idx(const int64_t& value) { this->idx = value; }

        const nlohmann::json& get_name() const { return name; }
        nlohmann::json& get_mutable_name() { return name; }
        void set_name(const nlohmann::json& value) { this->name = value; }

        const int64_t& get_shape() const { return shape; }
        int64_t& get_mutable_shape() { return shape; }
        void set_shape(const int64_t& value) { this->shape = value; }

        const std::vector<CenterLoc>& get_list_points() const { return list_points; }
        std::vector<CenterLoc>& get_mutable_list_points() { return list_points; }
        void set_list_points(const std::vector<CenterLoc>& value) { this->list_points = value; }

        const int64_t& get_point_count() const { return point_count; }
        int64_t& get_mutable_point_count() { return point_count; }
        void set_point_count(const int64_t& value) { this->point_count = value; }

        const bool& get_points_available() const { return points_available; }
        bool& get_mutable_points_available() { return points_available; }
        void set_points_available(const bool& value) { this->points_available = value; }

        const bool& get_data_saved() const { return data_saved; }
        bool& get_mutable_data_saved() { return data_saved; }
        void set_data_saved(const bool& value) { this->data_saved = value; }
    };

    class LstBoundaryGapTool {
    public:
        LstBoundaryGapTool() = default;
        virtual ~LstBoundaryGapTool() = default;

    private:
        FullRoi rect_roi;
        double gap_left;
        double gap_right;
        double gap_top;
        double gap_bottom;
        int64_t threshold;
        int64_t threshold_type;
        std::string name;
        CenterLoc center_loc;
        int64_t index;
        CenterLoc location_detected;
        bool tool_result;
        int64_t id;
        int64_t tool_type;
        ObjToolShape obj_tool_shape;

    public:
        const FullRoi& get_rect_roi() const { return rect_roi; }
        FullRoi& get_mutable_rect_roi() { return rect_roi; }
        void set_rect_roi(const FullRoi& value) { this->rect_roi = value; }

        const double& get_gap_left() const { return gap_left; }
        double& get_mutable_gap_left() { return gap_left; }
        void set_gap_left(const double& value) { this->gap_left = value; }

        const double& get_gap_right() const { return gap_right; }
        double& get_mutable_gap_right() { return gap_right; }
        void set_gap_right(const double& value) { this->gap_right = value; }

        const double& get_gap_top() const { return gap_top; }
        double& get_mutable_gap_top() { return gap_top; }
        void set_gap_top(const double& value) { this->gap_top = value; }

        const double& get_gap_bottom() const { return gap_bottom; }
        double& get_mutable_gap_bottom() { return gap_bottom; }
        void set_gap_bottom(const double& value) { this->gap_bottom = value; }

        const int64_t& get_threshold() const { return threshold; }
        int64_t& get_mutable_threshold() { return threshold; }
        void set_threshold(const int64_t& value) { this->threshold = value; }

        const int64_t& get_threshold_type() const { return threshold_type; }
        int64_t& get_mutable_threshold_type() { return threshold_type; }
        void set_threshold_type(const int64_t& value) { this->threshold_type = value; }

        const std::string& get_name() const { return name; }
        std::string& get_mutable_name() { return name; }
        void set_name(const std::string& value) { this->name = value; }

        const CenterLoc& get_center_loc() const { return center_loc; }
        CenterLoc& get_mutable_center_loc() { return center_loc; }
        void set_center_loc(const CenterLoc& value) { this->center_loc = value; }

        const int64_t& get_index() const { return index; }
        int64_t& get_mutable_index() { return index; }
        void set_index(const int64_t& value) { this->index = value; }

        const CenterLoc& get_location_detected() const { return location_detected; }
        CenterLoc& get_mutable_location_detected() { return location_detected; }
        void set_location_detected(const CenterLoc& value) { this->location_detected = value; }

        const bool& get_tool_result() const { return tool_result; }
        bool& get_mutable_tool_result() { return tool_result; }
        void set_tool_result(const bool& value) { this->tool_result = value; }

        const int64_t& get_id() const { return id; }
        int64_t& get_mutable_id() { return id; }
        void set_id(const int64_t& value) { this->id = value; }

        const int64_t& get_tool_type() const { return tool_type; }
        int64_t& get_mutable_tool_type() { return tool_type; }
        void set_tool_type(const int64_t& value) { this->tool_type = value; }

        const ObjToolShape& get_obj_tool_shape() const { return obj_tool_shape; }
        ObjToolShape& get_mutable_obj_tool_shape() { return obj_tool_shape; }
        void set_obj_tool_shape(const ObjToolShape& value) { this->obj_tool_shape = value; }
    };

    class LstCroiTool {
    public:
        LstCroiTool() = default;
        virtual ~LstCroiTool() = default;

    private:
        std::string template_name;
        double rotation_limit;
        double mathc_score_thresh;
        FullRoi rect_roi;
        FullRoi search_region;
        CenterLoc shift_tolerance;
        CenterLoc shift_tolerance_neg;
        int64_t mode;
        int64_t colour_id;
        int64_t threshold;
        int64_t threshold_type;
        std::string name;
        CenterLoc center_loc;
        int64_t index;
        CenterLoc location_detected;
        bool tool_result;
        int64_t id;
        int64_t tool_type;
        ObjToolShape obj_tool_shape;

    public:
        const std::string& get_template_name() const { return template_name; }
        std::string& get_mutable_template_name() { return template_name; }
        void set_template_name(const std::string& value) { this->template_name = value; }

        const double& get_rotation_limit() const { return rotation_limit; }
        double& get_mutable_rotation_limit() { return rotation_limit; }
        void set_rotation_limit(const double& value) { this->rotation_limit = value; }

        const double& get_mathc_score_thresh() const { return mathc_score_thresh; }
        double& get_mutable_mathc_score_thresh() { return mathc_score_thresh; }
        void set_mathc_score_thresh(const double& value) { this->mathc_score_thresh = value; }

        const FullRoi& get_rect_roi() const { return rect_roi; }
        FullRoi& get_mutable_rect_roi() { return rect_roi; }
        void set_rect_roi(const FullRoi& value) { this->rect_roi = value; }

        const FullRoi& get_search_region() const { return search_region; }
        FullRoi& get_mutable_search_region() { return search_region; }
        void set_search_region(const FullRoi& value) { this->search_region = value; }

        const CenterLoc& get_shift_tolerance() const { return shift_tolerance; }
        CenterLoc& get_mutable_shift_tolerance() { return shift_tolerance; }
        void set_shift_tolerance(const CenterLoc& value) { this->shift_tolerance = value; }

		const CenterLoc& get_shift_tolerance_neg() const { return shift_tolerance_neg; }
		CenterLoc& get_mutable_shift_tolerance_neg() { return shift_tolerance_neg; }
		void set_shift_tolerance_neg(const CenterLoc& value) { this->shift_tolerance_neg = value; }

        const int64_t& get_mode() const { return mode; }
        int64_t& get_mutable_mode() { return mode; }
        void set_mode(const int64_t& value) { this->mode = value; }

        const int64_t& get_colour_id() const { return colour_id; }
        int64_t& get_mutable_colour_id() { return colour_id; }
        void set_colour_id(const int64_t& value) { this->colour_id = value; }

        const int64_t& get_threshold() const { return threshold; }
        int64_t& get_mutable_threshold() { return threshold; }
        void set_threshold(const int64_t& value) { this->threshold = value; }

        const int64_t& get_threshold_type() const { return threshold_type; }
        int64_t& get_mutable_threshold_type() { return threshold_type; }
        void set_threshold_type(const int64_t& value) { this->threshold_type = value; }

        const std::string& get_name() const { return name; }
        std::string& get_mutable_name() { return name; }
        void set_name(const std::string& value) { this->name = value; }

        const CenterLoc& get_center_loc() const { return center_loc; }
        CenterLoc& get_mutable_center_loc() { return center_loc; }
        void set_center_loc(const CenterLoc& value) { this->center_loc = value; }

        const int64_t& get_index() const { return index; }
        int64_t& get_mutable_index() { return index; }
        void set_index(const int64_t& value) { this->index = value; }

        const CenterLoc& get_location_detected() const { return location_detected; }
        CenterLoc& get_mutable_location_detected() { return location_detected; }
        void set_location_detected(const CenterLoc& value) { this->location_detected = value; }

        const bool& get_tool_result() const { return tool_result; }
        bool& get_mutable_tool_result() { return tool_result; }
        void set_tool_result(const bool& value) { this->tool_result = value; }

        const int64_t& get_id() const { return id; }
        int64_t& get_mutable_id() { return id; }
        void set_id(const int64_t& value) { this->id = value; }

        const int64_t& get_tool_type() const { return tool_type; }
        int64_t& get_mutable_tool_type() { return tool_type; }
        void set_tool_type(const int64_t& value) { this->tool_type = value; }

        const ObjToolShape& get_obj_tool_shape() const { return obj_tool_shape; }
        ObjToolShape& get_mutable_obj_tool_shape() { return obj_tool_shape; }
        void set_obj_tool_shape(const ObjToolShape& value) { this->obj_tool_shape = value; }
    };

    class LstDatecodeTool {
    public:
        LstDatecodeTool() = default;
        virtual ~LstDatecodeTool() = default;

    private:
        FullRoi rect_roi;
        double min_dot_dia_mm;
        double max_dot_dia_mm;
        FullRoi rect_ocr;
        int64_t dot_cnt_left;
        int64_t dot_cnt_right;
        std::string mid_str;
        std::string name;
        CenterLoc center_loc;
        int64_t index;
        CenterLoc location_detected;
        bool tool_result;
        int64_t id;
        int64_t tool_type;
        ObjToolShape obj_tool_shape;

    public:
        const FullRoi& get_rect_roi() const { return rect_roi; }
        FullRoi& get_mutable_rect_roi() { return rect_roi; }
        void set_rect_roi(const FullRoi& value) { this->rect_roi = value; }

        const double& get_min_dot_dia_mm() const { return min_dot_dia_mm; }
        double& get_mutable_min_dot_dia_mm() { return min_dot_dia_mm; }
        void set_min_dot_dia_mm(const double& value) { this->min_dot_dia_mm = value; }

        const double& get_max_dot_dia_mm() const { return max_dot_dia_mm; }
        double& get_mutable_max_dot_dia_mm() { return max_dot_dia_mm; }
        void set_max_dot_dia_mm(const double& value) { this->max_dot_dia_mm = value; }

        const FullRoi& get_rect_ocr() const { return rect_ocr; }
        FullRoi& get_mutable_rect_ocr() { return rect_ocr; }
        void set_rect_ocr(const FullRoi& value) { this->rect_ocr = value; }

        const int64_t& get_dot_cnt_left() const { return dot_cnt_left; }
        int64_t& get_mutable_dot_cnt_left() { return dot_cnt_left; }
        void set_dot_cnt_left(const int64_t& value) { this->dot_cnt_left = value; }

        const int64_t& get_dot_cnt_right() const { return dot_cnt_right; }
        int64_t& get_mutable_dot_cnt_right() { return dot_cnt_right; }
        void set_dot_cnt_right(const int64_t& value) { this->dot_cnt_right = value; }

        const std::string& get_mid_str() const { return mid_str; }
        std::string& get_mutable_mid_str() { return mid_str; }
        void set_mid_str(const std::string& value) { this->mid_str = value; }

        const std::string& get_name() const { return name; }
        std::string& get_mutable_name() { return name; }
        void set_name(const std::string& value) { this->name = value; }

        const CenterLoc& get_center_loc() const { return center_loc; }
        CenterLoc& get_mutable_center_loc() { return center_loc; }
        void set_center_loc(const CenterLoc& value) { this->center_loc = value; }

        const int64_t& get_index() const { return index; }
        int64_t& get_mutable_index() { return index; }
        void set_index(const int64_t& value) { this->index = value; }

        const CenterLoc& get_location_detected() const { return location_detected; }
        CenterLoc& get_mutable_location_detected() { return location_detected; }
        void set_location_detected(const CenterLoc& value) { this->location_detected = value; }

        const bool& get_tool_result() const { return tool_result; }
        bool& get_mutable_tool_result() { return tool_result; }
        void set_tool_result(const bool& value) { this->tool_result = value; }

        const int64_t& get_id() const { return id; }
        int64_t& get_mutable_id() { return id; }
        void set_id(const int64_t& value) { this->id = value; }

        const int64_t& get_tool_type() const { return tool_type; }
        int64_t& get_mutable_tool_type() { return tool_type; }
        void set_tool_type(const int64_t& value) { this->tool_type = value; }

        const ObjToolShape& get_obj_tool_shape() const { return obj_tool_shape; }
        ObjToolShape& get_mutable_obj_tool_shape() { return obj_tool_shape; }
        void set_obj_tool_shape(const ObjToolShape& value) { this->obj_tool_shape = value; }
    };

    class LstFixtureTool {
    public:
        LstFixtureTool() = default;
        virtual ~LstFixtureTool() = default;

    private:
        std::string template_1__name;
        std::string template_2__name;
        double match_score_thresh_t1;
        int64_t match_score_thresh_t2;
        FullRoi rect_roi_t1;
        FullRoi rect_roi_t2;
        FullRoi rect_searc_region_t1;
        FullRoi rect_searc_region_t2;
        int64_t distance_bw_t1_t2;
        int64_t rotattion_limit_deg;
        std::string name;
        CenterLoc center_loc;
        int64_t index;
        CenterLoc location_detected;
        bool tool_result;
        int64_t id;
        int64_t tool_type;
        ObjToolShape obj_tool_shape;

    public:
        const std::string& get_template_1___name() const { return template_1__name; }
        std::string& get_mutable_template_1___name() { return template_1__name; }
        void set_template_1___name(const std::string& value) { this->template_1__name = value; }

        const std::string& get_template_2___name() const { return template_2__name; }
        std::string& get_mutable_template_2___name() { return template_2__name; }
        void set_template_2___name(const std::string& value) { this->template_2__name = value; }

        const double& get_match_score_thresh_t1() const { return match_score_thresh_t1; }
        double& get_mutable_match_score_thresh_t1() { return match_score_thresh_t1; }
        void set_match_score_thresh_t1(const double& value) { this->match_score_thresh_t1 = value; }

        const int64_t& get_match_score_thresh_t2() const { return match_score_thresh_t2; }
        int64_t& get_mutable_match_score_thresh_t2() { return match_score_thresh_t2; }
        void set_match_score_thresh_t2(const int64_t& value) { this->match_score_thresh_t2 = value; }

        const FullRoi& get_rect_roi_t1() const { return rect_roi_t1; }
        FullRoi& get_mutable_rect_roi_t1() { return rect_roi_t1; }
        void set_rect_roi_t1(const FullRoi& value) { this->rect_roi_t1 = value; }

        const FullRoi& get_rect_roi_t2() const { return rect_roi_t2; }
        FullRoi& get_mutable_rect_roi_t2() { return rect_roi_t2; }
        void set_rect_roi_t2(const FullRoi& value) { this->rect_roi_t2 = value; }

        const FullRoi& get_rect_searc_region_t1() const { return rect_searc_region_t1; }
        FullRoi& get_mutable_rect_searc_region_t1() { return rect_searc_region_t1; }
        void set_rect_searc_region_t1(const FullRoi& value) { this->rect_searc_region_t1 = value; }

        const FullRoi& get_rect_searc_region_t2() const { return rect_searc_region_t2; }
        FullRoi& get_mutable_rect_searc_region_t2() { return rect_searc_region_t2; }
        void set_rect_searc_region_t2(const FullRoi& value) { this->rect_searc_region_t2 = value; }

        const int64_t& get_distance_bw_t1_t2() const { return distance_bw_t1_t2; }
        int64_t& get_mutable_distance_bw_t1_t2() { return distance_bw_t1_t2; }
        void set_distance_bw_t1_t2(const int64_t& value) { this->distance_bw_t1_t2 = value; }

        const int64_t& get_rotattion_limit_deg() const { return rotattion_limit_deg; }
        int64_t& get_mutable_rotattion_limit_deg() { return rotattion_limit_deg; }
        void set_rotattion_limit_deg(const int64_t& value) { this->rotattion_limit_deg = value; }

        const std::string& get_name() const { return name; }
        std::string& get_mutable_name() { return name; }
        void set_name(const std::string& value) { this->name = value; }

        const CenterLoc& get_center_loc() const { return center_loc; }
        CenterLoc& get_mutable_center_loc() { return center_loc; }
        void set_center_loc(const CenterLoc& value) { this->center_loc = value; }

        const int64_t& get_index() const { return index; }
        int64_t& get_mutable_index() { return index; }
        void set_index(const int64_t& value) { this->index = value; }

        const CenterLoc& get_location_detected() const { return location_detected; }
        CenterLoc& get_mutable_location_detected() { return location_detected; }
        void set_location_detected(const CenterLoc& value) { this->location_detected = value; }

        const bool& get_tool_result() const { return tool_result; }
        bool& get_mutable_tool_result() { return tool_result; }
        void set_tool_result(const bool& value) { this->tool_result = value; }

        const int64_t& get_id() const { return id; }
        int64_t& get_mutable_id() { return id; }
        void set_id(const int64_t& value) { this->id = value; }

        const int64_t& get_tool_type() const { return tool_type; }
        int64_t& get_mutable_tool_type() { return tool_type; }
        void set_tool_type(const int64_t& value) { this->tool_type = value; }

        const ObjToolShape& get_obj_tool_shape() const { return obj_tool_shape; }
        ObjToolShape& get_mutable_obj_tool_shape() { return obj_tool_shape; }
        void set_obj_tool_shape(const ObjToolShape& value) { this->obj_tool_shape = value; }
    };

    class LstTool {
    public:
        LstTool() = default;
        virtual ~LstTool() = default;

    private:
        FullRoi rect_roi;
        boost::optional<int64_t> threshold;
        boost::optional<int64_t> threshold_type;
        boost::optional<int64_t> match_percent;
        std::string name;
        CenterLoc center_loc;
        int64_t index;
        CenterLoc location_detected;
        bool tool_result;
        int64_t id;
        int64_t tool_type;
        ObjToolShape obj_tool_shape;
        boost::optional<std::string> expected_string;
        boost::optional<std::string> result_string;
        boost::optional<double> min_dot_dia_mm;
        boost::optional<double> max_dot_dia_mm;
        boost::optional<int64_t> dot_cnt;
        boost::optional<std::string> template_name;
        boost::optional<int64_t> rotation_limit;
        boost::optional<double> mathc_score_thresh;

    public:
        const FullRoi& get_rect_roi() const { return rect_roi; }
        FullRoi& get_mutable_rect_roi() { return rect_roi; }
        void set_rect_roi(const FullRoi& value) { this->rect_roi = value; }

        boost::optional<int64_t> get_threshold() const { return threshold; }
        void set_threshold(boost::optional<int64_t> value) { this->threshold = value; }

        boost::optional<int64_t> get_threshold_type() const { return threshold_type; }
        void set_threshold_type(boost::optional<int64_t> value) { this->threshold_type = value; }

        boost::optional<int64_t> get_match_percent() const { return match_percent; }
        void set_match_percent(boost::optional<int64_t> value) { this->match_percent = value; }

        const std::string& get_name() const { return name; }
        std::string& get_mutable_name() { return name; }
        void set_name(const std::string& value) { this->name = value; }

        const CenterLoc& get_center_loc() const { return center_loc; }
        CenterLoc& get_mutable_center_loc() { return center_loc; }
        void set_center_loc(const CenterLoc& value) { this->center_loc = value; }

        const int64_t& get_index() const { return index; }
        int64_t& get_mutable_index() { return index; }
        void set_index(const int64_t& value) { this->index = value; }

        const CenterLoc& get_location_detected() const { return location_detected; }
        CenterLoc& get_mutable_location_detected() { return location_detected; }
        void set_location_detected(const CenterLoc& value) { this->location_detected = value; }

        const bool& get_tool_result() const { return tool_result; }
        bool& get_mutable_tool_result() { return tool_result; }
        void set_tool_result(const bool& value) { this->tool_result = value; }

        const int64_t& get_id() const { return id; }
        int64_t& get_mutable_id() { return id; }
        void set_id(const int64_t& value) { this->id = value; }

        const int64_t& get_tool_type() const { return tool_type; }
        int64_t& get_mutable_tool_type() { return tool_type; }
        void set_tool_type(const int64_t& value) { this->tool_type = value; }

        const ObjToolShape& get_obj_tool_shape() const { return obj_tool_shape; }
        ObjToolShape& get_mutable_obj_tool_shape() { return obj_tool_shape; }
        void set_obj_tool_shape(const ObjToolShape& value) { this->obj_tool_shape = value; }

        boost::optional<std::string> get_expected_string() const { return expected_string; }
        void set_expected_string(boost::optional<std::string> value) { this->expected_string = value; }

        boost::optional<std::string> get_result_string() const { return result_string; }
        void set_result_string(boost::optional<std::string> value) { this->result_string = value; }

        boost::optional<double> get_min_dot_dia_mm() const { return min_dot_dia_mm; }
        void set_min_dot_dia_mm(boost::optional<double> value) { this->min_dot_dia_mm = value; }

        boost::optional<double> get_max_dot_dia_mm() const { return max_dot_dia_mm; }
        void set_max_dot_dia_mm(boost::optional<double> value) { this->max_dot_dia_mm = value; }

        boost::optional<int64_t> get_dot_cnt() const { return dot_cnt; }
        void set_dot_cnt(boost::optional<int64_t> value) { this->dot_cnt = value; }

        boost::optional<std::string> get_template_name() const { return template_name; }
        void set_template_name(boost::optional<std::string> value) { this->template_name = value; }

        boost::optional<int64_t> get_rotation_limit() const { return rotation_limit; }
        void set_rotation_limit(boost::optional<int64_t> value) { this->rotation_limit = value; }

        boost::optional<double> get_mathc_score_thresh() const { return mathc_score_thresh; }
        void set_mathc_score_thresh(boost::optional<double> value) { this->mathc_score_thresh = value; }
    };

    class LstGroupPrintCheckTool {
    public:
        LstGroupPrintCheckTool() = default;
        virtual ~LstGroupPrintCheckTool() = default;

    private:
        double boundary_thickness;
        double threshold;
        double burn_threshold;
        double heigh_tolerance;
        double width_tolerance;
        double area_tolerance;
        double shift_x_tol;
        double shift_y_tol;
        FullRoi rect_roi;
        std::string template_name;
        std::string name;
        CenterLoc center_loc;
        int64_t index;
        CenterLoc location_detected;
        bool tool_result;
        int64_t id;
        int64_t tool_type;
        ObjToolShape obj_tool_shape;

    public:
        const double& get_boundary_thickness() const { return boundary_thickness; }
        double& get_mutable_boundary_thickness() { return boundary_thickness; }
        void set_boundary_thickness(const double& value) { this->boundary_thickness = value; }

        const double& get_threshold() const { return threshold; }
        double& get_mutable_threshold() { return threshold; }
        void set_threshold(const double& value) { this->threshold = value; }

        const double& get_burn_threshold() const { return burn_threshold; }
        double& get_mutable_burn_threshold() { return burn_threshold; }
        void set_burn_threshold(const double& value) { this->burn_threshold = value; }

        const double& get_heigh_tolerance() const { return heigh_tolerance; }
        double& get_mutable_heigh_tolerance() { return heigh_tolerance; }
        void set_heigh_tolerance(const double& value) { this->heigh_tolerance = value; }

        const double& get_width_tolerance() const { return width_tolerance; }
        double& get_mutable_width_tolerance() { return width_tolerance; }
        void set_width_tolerance(const double& value) { this->width_tolerance = value; }

        const double& get_area_tolerance() const { return area_tolerance; }
        double& get_mutable_area_tolerance() { return area_tolerance; }
        void set_area_tolerance(const double& value) { this->area_tolerance = value; }

        const double& get_shift_x_tol() const { return shift_x_tol; }
        double& get_mutable_shift_x_tol() { return shift_x_tol; }
        void set_shift_x_tol(const double& value) { this->shift_x_tol = value; }

        const double& get_shift_y_tol() const { return shift_y_tol; }
        double& get_mutable_shift_y_tol() { return shift_y_tol; }
        void set_shift_y_tol(const double& value) { this->shift_y_tol = value; }

        const FullRoi& get_rect_roi() const { return rect_roi; }
        FullRoi& get_mutable_rect_roi() { return rect_roi; }
        void set_rect_roi(const FullRoi& value) { this->rect_roi = value; }

        const std::string& get_template_name() const { return template_name; }
        std::string& get_mutable_template_name() { return template_name; }
        void set_template_name(const std::string& value) { this->template_name = value; }

        const std::string& get_name() const { return name; }
        std::string& get_mutable_name() { return name; }
        void set_name(const std::string& value) { this->name = value; }

        const CenterLoc& get_center_loc() const { return center_loc; }
        CenterLoc& get_mutable_center_loc() { return center_loc; }
        void set_center_loc(const CenterLoc& value) { this->center_loc = value; }

        const int64_t& get_index() const { return index; }
        int64_t& get_mutable_index() { return index; }
        void set_index(const int64_t& value) { this->index = value; }

        const CenterLoc& get_location_detected() const { return location_detected; }
        CenterLoc& get_mutable_location_detected() { return location_detected; }
        void set_location_detected(const CenterLoc& value) { this->location_detected = value; }

        const bool& get_tool_result() const { return tool_result; }
        bool& get_mutable_tool_result() { return tool_result; }
        void set_tool_result(const bool& value) { this->tool_result = value; }

        const int64_t& get_id() const { return id; }
        int64_t& get_mutable_id() { return id; }
        void set_id(const int64_t& value) { this->id = value; }

        const int64_t& get_tool_type() const { return tool_type; }
        int64_t& get_mutable_tool_type() { return tool_type; }
        void set_tool_type(const int64_t& value) { this->tool_type = value; }

        const ObjToolShape& get_obj_tool_shape() const { return obj_tool_shape; }
        ObjToolShape& get_mutable_obj_tool_shape() { return obj_tool_shape; }
        void set_obj_tool_shape(const ObjToolShape& value) { this->obj_tool_shape = value; }
    };

    class MarkInsTools {
    public:
        MarkInsTools() = default;
        virtual ~MarkInsTools() = default;

    private:
        std::string mark_configname;
        int64_t mark_config_id;
        int64_t sub_p_cno;
        FullRoi full_roi;
        int64_t roi_rotation;
        int64_t roi_flip_mode;
        std::string roi_file_name;
        std::string glass_template_file_name;
        std::string rotated_roi_file_name;
        FullRoi glass_location;
        int64_t logo_shift_tol_x;
        int64_t logo_shift_tol_y;
        std::vector<LstTool> lst_roi_tools;
        std::vector<LstTool> lst_qr_tools;
        std::vector<LstGroupPrintCheckTool> lst_group_print_check_tools;
        std::vector<LstDatecodeTool> lst_datecode_tools;
        std::vector<LstTool> lst_week_code_tools;
        std::vector<LstTool> lst_mask_tools;
        std::vector<LstFixtureTool> lst_fixture_tools;
        std::vector<LstCroiTool> lst_croi_tool;
        std::vector<LstBoundaryGapTool> lst_boundary_gap_tools;
        std::vector<LstTool> lst_gray_presence_tools;
        int64_t applied_tool_cnt;

    public:
        const std::string& get_mark_configname() const { return mark_configname; }
        std::string& get_mutable_mark_configname() { return mark_configname; }
        void set_mark_configname(const std::string& value) { this->mark_configname = value; }

        const int64_t& get_mark_config_id() const { return mark_config_id; }
        int64_t& get_mutable_mark_config_id() { return mark_config_id; }
        void set_mark_config_id(const int64_t& value) { this->mark_config_id = value; }

        const int64_t& get_sub_p_cno() const { return sub_p_cno; }
        int64_t& get_mutable_sub_p_cno() { return sub_p_cno; }
        void set_sub_p_cno(const int64_t& value) { this->sub_p_cno = value; }

        const FullRoi& get_full_roi() const { return full_roi; }
        FullRoi& get_mutable_full_roi() { return full_roi; }
        void set_full_roi(const FullRoi& value) { this->full_roi = value; }

        const int64_t& get_roi_rotation() const { return roi_rotation; }
        int64_t& get_mutable_roi_rotation() { return roi_rotation; }
        void set_roi_rotation(const int64_t& value) { this->roi_rotation = value; }

        const int64_t& get_roi_flip_mode() const { return roi_flip_mode; }
        int64_t& get_mutable_roi_flip_mode() { return roi_flip_mode; }
        void set_roi_flip_mode(const int64_t& value) { this->roi_flip_mode = value; }

        const std::string& get_roi_file_name() const { return roi_file_name; }
        std::string& get_mutable_roi_file_name() { return roi_file_name; }
        void set_roi_file_name(const std::string& value) { this->roi_file_name = value; }

        const std::string& get_glass_template_file_name() const { return glass_template_file_name; }
        std::string& get_mutable_glass_template_file_name() { return glass_template_file_name; }
        void set_glass_template_file_name(const std::string& value) { this->glass_template_file_name = value; }

        const std::string& get_rotated_roi_file_name() const { return rotated_roi_file_name; }
        std::string& get_mutable_rotated_roi_file_name() { return rotated_roi_file_name; }
        void set_rotated_roi_file_name(const std::string& value) { this->rotated_roi_file_name = value; }

        const FullRoi& get_glass_location() const { return glass_location; }
        FullRoi& get_mutable_glass_location() { return glass_location; }
        void set_glass_location(const FullRoi& value) { this->glass_location = value; }

        const int64_t& get_logo_shift_tol_x() const { return logo_shift_tol_x; }
        int64_t& get_mutable_logo_shift_tol_x() { return logo_shift_tol_x; }
        void set_logo_shift_tol_x(const int64_t& value) { this->logo_shift_tol_x = value; }

        const int64_t& get_logo_shift_tol_y() const { return logo_shift_tol_y; }
        int64_t& get_mutable_logo_shift_tol_y() { return logo_shift_tol_y; }
        void set_logo_shift_tol_y(const int64_t& value) { this->logo_shift_tol_y = value; }

        const std::vector<LstTool>& get_lst_roi_tools() const { return lst_roi_tools; }
        std::vector<LstTool>& get_mutable_lst_roi_tools() { return lst_roi_tools; }
        void set_lst_roi_tools(const std::vector<LstTool>& value) { this->lst_roi_tools = value; }

        const std::vector<LstTool>& get_lst_qr_tools() const { return lst_qr_tools; }
        std::vector<LstTool>& get_mutable_lst_qr_tools() { return lst_qr_tools; }
        void set_lst_qr_tools(const std::vector<LstTool>& value) { this->lst_qr_tools = value; }

        const std::vector<LstGroupPrintCheckTool>& get_lst_group_print_check_tools() const { return lst_group_print_check_tools; }
        std::vector<LstGroupPrintCheckTool>& get_mutable_lst_group_print_check_tools() { return lst_group_print_check_tools; }
        void set_lst_group_print_check_tools(const std::vector<LstGroupPrintCheckTool>& value) { this->lst_group_print_check_tools = value; }

        const std::vector<LstDatecodeTool>& get_lst_datecode_tools() const { return lst_datecode_tools; }
        std::vector<LstDatecodeTool>& get_mutable_lst_datecode_tools() { return lst_datecode_tools; }
        void set_lst_datecode_tools(const std::vector<LstDatecodeTool>& value) { this->lst_datecode_tools = value; }

        const std::vector<LstTool>& get_lst_week_code_tools() const { return lst_week_code_tools; }
        std::vector<LstTool>& get_mutable_lst_week_code_tools() { return lst_week_code_tools; }
        void set_lst_week_code_tools(const std::vector<LstTool>& value) { this->lst_week_code_tools = value; }

        const std::vector<LstTool>& get_lst_mask_tools() const { return lst_mask_tools; }
        std::vector<LstTool>& get_mutable_lst_mask_tools() { return lst_mask_tools; }
        void set_lst_mask_tools(const std::vector<LstTool>& value) { this->lst_mask_tools = value; }

        const std::vector<LstFixtureTool>& get_lst_fixture_tools() const { return lst_fixture_tools; }
        std::vector<LstFixtureTool>& get_mutable_lst_fixture_tools() { return lst_fixture_tools; }
        void set_lst_fixture_tools(const std::vector<LstFixtureTool>& value) { this->lst_fixture_tools = value; }

        const std::vector<LstCroiTool>& get_lst_croi_tool() const { return lst_croi_tool; }
        std::vector<LstCroiTool>& get_mutable_lst_croi_tool() { return lst_croi_tool; }
        void set_lst_croi_tool(const std::vector<LstCroiTool>& value) { this->lst_croi_tool = value; }

        const std::vector<LstBoundaryGapTool>& get_lst_boundary_gap_tools() const { return lst_boundary_gap_tools; }
        std::vector<LstBoundaryGapTool>& get_mutable_lst_boundary_gap_tools() { return lst_boundary_gap_tools; }
        void set_lst_boundary_gap_tools(const std::vector<LstBoundaryGapTool>& value) { this->lst_boundary_gap_tools = value; }

        const std::vector<LstTool>& get_lst_gray_presence_tools() const { return lst_gray_presence_tools; }
        std::vector<LstTool>& get_mutable_lst_gray_presence_tools() { return lst_gray_presence_tools; }
        void set_lst_gray_presence_tools(const std::vector<LstTool>& value) { this->lst_gray_presence_tools = value; }

        const int64_t& get_applied_tool_cnt() const { return applied_tool_cnt; }
        int64_t& get_mutable_applied_tool_cnt() { return applied_tool_cnt; }
        void set_applied_tool_cnt(const int64_t& value) { this->applied_tool_cnt = value; }
    };

    void from_json(const json& j, FullRoi& x);
    void to_json(json& j, const FullRoi& x);

    void from_json(const json& j, CenterLoc& x);
    void to_json(json& j, const CenterLoc& x);

    void from_json(const json& j, ObjToolShape& x);
    void to_json(json& j, const ObjToolShape& x);

    void from_json(const json& j, LstBoundaryGapTool& x);
    void to_json(json& j, const LstBoundaryGapTool& x);

    void from_json(const json& j, LstCroiTool& x);
    void to_json(json& j, const LstCroiTool& x);

    void from_json(const json& j, LstDatecodeTool& x);
    void to_json(json& j, const LstDatecodeTool& x);

    void from_json(const json& j, LstFixtureTool& x);
    void to_json(json& j, const LstFixtureTool& x);

    void from_json(const json& j, LstTool& x);
    void to_json(json& j, const LstTool& x);

    void from_json(const json& j, LstGroupPrintCheckTool& x);
    void to_json(json& j, const LstGroupPrintCheckTool& x);

    void from_json(const json& j, MarkInsTools& x);
    void to_json(json& j, const MarkInsTools& x);

    inline void from_json(const json& j, FullRoi& x) {
        x.set_x(j.at("X").get<int64_t>());
        x.set_y(j.at("Y").get<int64_t>());
        x.set_width(j.at("Width").get<int64_t>());
        x.set_height(j.at("Height").get<int64_t>());
    }

    inline void to_json(json& j, const FullRoi& x) {
        j = json::object();
        j["X"] = x.get_x();
        j["Y"] = x.get_y();
        j["Width"] = x.get_width();
        j["Height"] = x.get_height();
    }

    inline void from_json(const json& j, CenterLoc& x) {
        x.set_x(j.at("X").get<double>());
        x.set_y(j.at("Y").get<double>());
    }

    inline void to_json(json& j, const CenterLoc& x) {
        j = json::object();
        j["X"] = x.get_x();
        j["Y"] = x.get_y();
    }

    inline void from_json(const json& j, ObjToolShape& x) {
        x.set_idx(j.at("idx").get<int64_t>());
        x.set_name(get_untyped(j, "name"));
        x.set_shape(j.at("shape").get<int64_t>());
        x.set_list_points(j.at("list_points").get<std::vector<CenterLoc>>());
        x.set_point_count(j.at("pointCount").get<int64_t>());
        x.set_points_available(j.at("pointsAvailable").get<bool>());
        x.set_data_saved(j.at("dataSaved").get<bool>());
    }

    inline void to_json(json& j, const ObjToolShape& x) {
        j = json::object();
        j["idx"] = x.get_idx();
        j["name"] = x.get_name();
        j["shape"] = x.get_shape();
        j["list_points"] = x.get_list_points();
        j["pointCount"] = x.get_point_count();
        j["pointsAvailable"] = x.get_points_available();
        j["dataSaved"] = x.get_data_saved();
    }

    inline void from_json(const json& j, LstBoundaryGapTool& x) {
        x.set_rect_roi(j.at("Rect_roi").get<FullRoi>());
        x.set_gap_left(j.at("GapLeft").get<double>());
        x.set_gap_right(j.at("GapRight").get<double>());
        x.set_gap_top(j.at("GapTop").get<double>());
        x.set_gap_bottom(j.at("GapBottom").get<double>());
        x.set_threshold(j.at("Threshold").get<int64_t>());
        x.set_threshold_type(j.at("ThresholdType").get<int64_t>());
        x.set_name(j.at("Name").get<std::string>());
        x.set_center_loc(j.at("CenterLoc").get<CenterLoc>());
        x.set_index(j.at("Index").get<int64_t>());
        x.set_location_detected(j.at("LocationDetected").get<CenterLoc>());
        x.set_tool_result(j.at("Tool_result").get<bool>());
        x.set_id(j.at("Id").get<int64_t>());
        x.set_tool_type(j.at("Tool_type").get<int64_t>());
        x.set_obj_tool_shape(j.at("Obj_toolShape").get<ObjToolShape>());
    }

    inline void to_json(json& j, const LstBoundaryGapTool& x) {
        j = json::object();
        j["Rect_roi"] = x.get_rect_roi();
        j["GapLeft"] = x.get_gap_left();
        j["GapRight"] = x.get_gap_right();
        j["GapTop"] = x.get_gap_top();
        j["GapBottom"] = x.get_gap_bottom();
        j["Threshold"] = x.get_threshold();
        j["ThresholdType"] = x.get_threshold_type();
        j["Name"] = x.get_name();
        j["CenterLoc"] = x.get_center_loc();
        j["Index"] = x.get_index();
        j["LocationDetected"] = x.get_location_detected();
        j["Tool_result"] = x.get_tool_result();
        j["Id"] = x.get_id();
        j["Tool_type"] = x.get_tool_type();
        j["Obj_toolShape"] = x.get_obj_tool_shape();
    }

    inline void from_json(const json& j, LstCroiTool& x) {
        x.set_template_name(j.at("TemplateName").get<std::string>());
        x.set_rotation_limit(j.at("RotationLimit").get<double>());
        x.set_mathc_score_thresh(j.at("MathcScoreThresh").get<double>());
        x.set_rect_roi(j.at("Rect_roi").get<FullRoi>());
        x.set_search_region(j.at("SearchRegion").get<FullRoi>());
        x.set_shift_tolerance(j.at("ShiftTolerance").get<CenterLoc>());
        x.set_shift_tolerance_neg(j.at("ShiftTolerance_neg").get<CenterLoc>());
        x.set_mode(j.at("Mode").get<int64_t>());
        x.set_colour_id(j.at("ColourId").get<int64_t>());
        x.set_threshold(j.at("Threshold").get<int64_t>());
        x.set_threshold_type(j.at("ThresholdType").get<int64_t>());
        x.set_name(j.at("Name").get<std::string>());
        x.set_center_loc(j.at("CenterLoc").get<CenterLoc>());
        x.set_index(j.at("Index").get<int64_t>());
        x.set_location_detected(j.at("LocationDetected").get<CenterLoc>());
        x.set_tool_result(j.at("Tool_result").get<bool>());
        x.set_id(j.at("Id").get<int64_t>());
        x.set_tool_type(j.at("Tool_type").get<int64_t>());
        x.set_obj_tool_shape(j.at("Obj_toolShape").get<ObjToolShape>());
    }

    inline void to_json(json& j, const LstCroiTool& x) {
        j = json::object();
        j["TemplateName"] = x.get_template_name();
        j["RotationLimit"] = x.get_rotation_limit();
        j["MathcScoreThresh"] = x.get_mathc_score_thresh();
        j["Rect_roi"] = x.get_rect_roi();
        j["SearchRegion"] = x.get_search_region();
        j["ShiftTolerance"] = x.get_shift_tolerance();
        j["ShiftTolerance_neg"] = x.get_shift_tolerance_neg();
        j["Mode"] = x.get_mode();
        j["ColourId"] = x.get_colour_id();
        j["Threshold"] = x.get_threshold();
        j["ThresholdType"] = x.get_threshold_type();
        j["Name"] = x.get_name();
        j["CenterLoc"] = x.get_center_loc();
        j["Index"] = x.get_index();
        j["LocationDetected"] = x.get_location_detected();
        j["Tool_result"] = x.get_tool_result();
        j["Id"] = x.get_id();
        j["Tool_type"] = x.get_tool_type();
        j["Obj_toolShape"] = x.get_obj_tool_shape();
    }

    inline void from_json(const json& j, LstDatecodeTool& x) {
        x.set_rect_roi(j.at("Rect_roi").get<FullRoi>());
        x.set_min_dot_dia_mm(j.at("MinDotDia_mm").get<double>());
        x.set_max_dot_dia_mm(j.at("MaxDotDia_mm").get<double>());
        x.set_rect_ocr(j.at("Rect_OCR").get<FullRoi>());
        x.set_dot_cnt_left(j.at("DotCntLeft").get<int64_t>());
        x.set_dot_cnt_right(j.at("DotCntRight").get<int64_t>());
        x.set_mid_str(j.at("MidStr").get<std::string>());
        x.set_name(j.at("Name").get<std::string>());
        x.set_center_loc(j.at("CenterLoc").get<CenterLoc>());
        x.set_index(j.at("Index").get<int64_t>());
        x.set_location_detected(j.at("LocationDetected").get<CenterLoc>());
        x.set_tool_result(j.at("Tool_result").get<bool>());
        x.set_id(j.at("Id").get<int64_t>());
        x.set_tool_type(j.at("Tool_type").get<int64_t>());
        x.set_obj_tool_shape(j.at("Obj_toolShape").get<ObjToolShape>());
    }

    inline void to_json(json& j, const LstDatecodeTool& x) {
        j = json::object();
        j["Rect_roi"] = x.get_rect_roi();
        j["MinDotDia_mm"] = x.get_min_dot_dia_mm();
        j["MaxDotDia_mm"] = x.get_max_dot_dia_mm();
        j["Rect_OCR"] = x.get_rect_ocr();
        j["DotCntLeft"] = x.get_dot_cnt_left();
        j["DotCntRight"] = x.get_dot_cnt_right();
        j["MidStr"] = x.get_mid_str();
        j["Name"] = x.get_name();
        j["CenterLoc"] = x.get_center_loc();
        j["Index"] = x.get_index();
        j["LocationDetected"] = x.get_location_detected();
        j["Tool_result"] = x.get_tool_result();
        j["Id"] = x.get_id();
        j["Tool_type"] = x.get_tool_type();
        j["Obj_toolShape"] = x.get_obj_tool_shape();
    }

    inline void from_json(const json& j, LstFixtureTool& x) {
        x.set_template_1___name(j.at("Template_1_Name").get<std::string>());
        x.set_template_2___name(j.at("Template_2_Name").get<std::string>());
        x.set_match_score_thresh_t1(j.at("MatchScoreThresh_T1").get<double>());
        x.set_match_score_thresh_t2(j.at("MatchScoreThresh_T2").get<int64_t>());
        x.set_rect_roi_t1(j.at("RectRoi_T1").get<FullRoi>());
        x.set_rect_roi_t2(j.at("RectRoi_T2").get<FullRoi>());
        x.set_rect_searc_region_t1(j.at("RectSearcRegion_T1").get<FullRoi>());
        x.set_rect_searc_region_t2(j.at("RectSearcRegion_T2").get<FullRoi>());
        x.set_distance_bw_t1_t2(j.at("DistanceBw_T1_T2").get<int64_t>());
        x.set_rotattion_limit_deg(j.at("RotattionLimit_deg").get<int64_t>());
        x.set_name(j.at("Name").get<std::string>());
        x.set_center_loc(j.at("CenterLoc").get<CenterLoc>());
        x.set_index(j.at("Index").get<int64_t>());
        x.set_location_detected(j.at("LocationDetected").get<CenterLoc>());
        x.set_tool_result(j.at("Tool_result").get<bool>());
        x.set_id(j.at("Id").get<int64_t>());
        x.set_tool_type(j.at("Tool_type").get<int64_t>());
        x.set_obj_tool_shape(j.at("Obj_toolShape").get<ObjToolShape>());
    }

    inline void to_json(json& j, const LstFixtureTool& x) {
        j = json::object();
        j["Template_1_Name"] = x.get_template_1___name();
        j["Template_2_Name"] = x.get_template_2___name();
        j["MatchScoreThresh_T1"] = x.get_match_score_thresh_t1();
        j["MatchScoreThresh_T2"] = x.get_match_score_thresh_t2();
        j["RectRoi_T1"] = x.get_rect_roi_t1();
        j["RectRoi_T2"] = x.get_rect_roi_t2();
        j["RectSearcRegion_T1"] = x.get_rect_searc_region_t1();
        j["RectSearcRegion_T2"] = x.get_rect_searc_region_t2();
        j["DistanceBw_T1_T2"] = x.get_distance_bw_t1_t2();
        j["RotattionLimit_deg"] = x.get_rotattion_limit_deg();
        j["Name"] = x.get_name();
        j["CenterLoc"] = x.get_center_loc();
        j["Index"] = x.get_index();
        j["LocationDetected"] = x.get_location_detected();
        j["Tool_result"] = x.get_tool_result();
        j["Id"] = x.get_id();
        j["Tool_type"] = x.get_tool_type();
        j["Obj_toolShape"] = x.get_obj_tool_shape();
    }

    inline void from_json(const json& j, LstTool& x) {
        x.set_rect_roi(j.at("Rect_roi").get<FullRoi>());
        x.set_threshold(get_stack_optional<int64_t>(j, "Threshold"));
        x.set_threshold_type(get_stack_optional<int64_t>(j, "ThresholdType"));
        x.set_match_percent(get_stack_optional<int64_t>(j, "MatchPercent"));
        x.set_name(j.at("Name").get<std::string>());
        x.set_center_loc(j.at("CenterLoc").get<CenterLoc>());
        x.set_index(j.at("Index").get<int64_t>());
        x.set_location_detected(j.at("LocationDetected").get<CenterLoc>());
        x.set_tool_result(j.at("Tool_result").get<bool>());
        x.set_id(j.at("Id").get<int64_t>());
        x.set_tool_type(j.at("Tool_type").get<int64_t>());
        x.set_obj_tool_shape(j.at("Obj_toolShape").get<ObjToolShape>());
        x.set_expected_string(get_stack_optional<std::string>(j, "ExpectedString"));
        x.set_result_string(get_stack_optional<std::string>(j, "ResultString"));
        x.set_min_dot_dia_mm(get_stack_optional<double>(j, "MinDotDia_mm"));
        x.set_max_dot_dia_mm(get_stack_optional<double>(j, "MaxDotDia_mm"));
        x.set_dot_cnt(get_stack_optional<int64_t>(j, "DotCnt"));
        x.set_template_name(get_stack_optional<std::string>(j, "TemplateName"));
        x.set_rotation_limit(get_stack_optional<int64_t>(j, "RotationLimit"));
        x.set_mathc_score_thresh(get_stack_optional<double>(j, "MathcScoreThresh"));
    }

    inline void to_json(json& j, const LstTool& x) {
        j = json::object();
        j["Rect_roi"] = x.get_rect_roi();
        j["Threshold"] = x.get_threshold();
        j["ThresholdType"] = x.get_threshold_type();
        j["MatchPercent"] = x.get_match_percent();
        j["Name"] = x.get_name();
        j["CenterLoc"] = x.get_center_loc();
        j["Index"] = x.get_index();
        j["LocationDetected"] = x.get_location_detected();
        j["Tool_result"] = x.get_tool_result();
        j["Id"] = x.get_id();
        j["Tool_type"] = x.get_tool_type();
        j["Obj_toolShape"] = x.get_obj_tool_shape();
        j["ExpectedString"] = x.get_expected_string();
        j["ResultString"] = x.get_result_string();
        j["MinDotDia_mm"] = x.get_min_dot_dia_mm();
        j["MaxDotDia_mm"] = x.get_max_dot_dia_mm();
        j["DotCnt"] = x.get_dot_cnt();
        j["TemplateName"] = x.get_template_name();
        j["RotationLimit"] = x.get_rotation_limit();
        j["MathcScoreThresh"] = x.get_mathc_score_thresh();
    }

    inline void from_json(const json& j, LstGroupPrintCheckTool& x) {
        x.set_boundary_thickness(j.at("BoundaryThickness").get<double>());
        x.set_threshold(j.at("Threshold").get<double>());
        x.set_burn_threshold(j.at("burnThreshold").get<double>());
        x.set_heigh_tolerance(j.at("HeighTolerance").get<double>());
        x.set_width_tolerance(j.at("WidthTolerance").get<double>());
        x.set_area_tolerance(j.at("AreaTolerance").get<double>());
        x.set_shift_x_tol(j.at("shiftXTol").get<double>());
        x.set_shift_y_tol(j.at("shiftYTol").get<double>());
        x.set_rect_roi(j.at("Rect_roi").get<FullRoi>());
        x.set_template_name(j.at("TemplateName").get<std::string>());
        x.set_name(j.at("Name").get<std::string>());
        x.set_center_loc(j.at("CenterLoc").get<CenterLoc>());
        x.set_index(j.at("Index").get<int64_t>());
        x.set_location_detected(j.at("LocationDetected").get<CenterLoc>());
        x.set_tool_result(j.at("Tool_result").get<bool>());
        x.set_id(j.at("Id").get<int64_t>());
        x.set_tool_type(j.at("Tool_type").get<int64_t>());
        x.set_obj_tool_shape(j.at("Obj_toolShape").get<ObjToolShape>());
    }

    inline void to_json(json& j, const LstGroupPrintCheckTool& x) {
        j = json::object();
        j["BoundaryThickness"] = x.get_boundary_thickness();
        j["Threshold"] = x.get_threshold();
        j["burnThreshold"] = x.get_burn_threshold();
        j["HeighTolerance"] = x.get_heigh_tolerance();
        j["WidthTolerance"] = x.get_width_tolerance();
        j["AreaTolerance"] = x.get_area_tolerance();
        j["shiftXTol"] = x.get_shift_x_tol();
        j["shiftYTol"] = x.get_shift_y_tol();
        j["Rect_roi"] = x.get_rect_roi();
        j["TemplateName"] = x.get_template_name();
        j["Name"] = x.get_name();
        j["CenterLoc"] = x.get_center_loc();
        j["Index"] = x.get_index();
        j["LocationDetected"] = x.get_location_detected();
        j["Tool_result"] = x.get_tool_result();
        j["Id"] = x.get_id();
        j["Tool_type"] = x.get_tool_type();
        j["Obj_toolShape"] = x.get_obj_tool_shape();
    }

    inline void from_json(const json& j, MarkInsTools& x) {
        x.set_mark_configname(j.at("markConfigname").get<std::string>());
        x.set_mark_config_id(j.at("markConfigId").get<int64_t>());
        x.set_sub_p_cno(j.at("subPCno").get<int64_t>());
        x.set_full_roi(j.at("fullROI").get<FullRoi>());
        x.set_roi_rotation(j.at("roi_rotation").get<int64_t>());
        x.set_roi_flip_mode(j.at("roi_flipMode").get<int64_t>());
        x.set_roi_file_name(j.at("roi_fileName").get<std::string>());
        x.set_glass_template_file_name(j.at("glass_templateFileName").get<std::string>());
        x.set_rotated_roi_file_name(j.at("rotatedROIFileName").get<std::string>());
        x.set_glass_location(j.at("glassLocation").get<FullRoi>());
        x.set_logo_shift_tol_x(j.at("logoShiftTolX").get<int64_t>());
        x.set_logo_shift_tol_y(j.at("logoShiftTolY").get<int64_t>());
        x.set_lst_roi_tools(j.at("lst_roiTools").get<std::vector<LstTool>>());
        x.set_lst_qr_tools(j.at("lst_QR_tools").get<std::vector<LstTool>>());
        x.set_lst_group_print_check_tools(j.at("lst_Group_printCheck_tools").get<std::vector<LstGroupPrintCheckTool>>());
        x.set_lst_datecode_tools(j.at("lst_Datecode_tools").get<std::vector<LstDatecodeTool>>());
        x.set_lst_week_code_tools(j.at("lst_WeekCode_tools").get<std::vector<LstTool>>());
        x.set_lst_mask_tools(j.at("lst_Mask_tools").get<std::vector<LstTool>>());
        x.set_lst_fixture_tools(j.at("lst_Fixture_tools").get<std::vector<LstFixtureTool>>());
        x.set_lst_croi_tool(j.at("lst_CROI_tool").get<std::vector<LstCroiTool>>());
        x.set_lst_boundary_gap_tools(j.at("lst_BoundaryGap_tools").get<std::vector<LstBoundaryGapTool>>());
        x.set_lst_gray_presence_tools(j.at("lst_GrayPresence_tools").get<std::vector<LstTool>>());
        x.set_applied_tool_cnt(j.at("AppliedToolCnt").get<int64_t>());
    }

    inline void to_json(json& j, const MarkInsTools& x) {
        j = json::object();
        j["markConfigname"] = x.get_mark_configname();
        j["markConfigId"] = x.get_mark_config_id();
        j["subPCno"] = x.get_sub_p_cno();
        j["fullROI"] = x.get_full_roi();
        j["roi_rotation"] = x.get_roi_rotation();
        j["roi_flipMode"] = x.get_roi_flip_mode();
        j["roi_fileName"] = x.get_roi_file_name();
        j["glass_templateFileName"] = x.get_glass_template_file_name();
        j["rotatedROIFileName"] = x.get_rotated_roi_file_name();
        j["glassLocation"] = x.get_glass_location();
        j["logoShiftTolX"] = x.get_logo_shift_tol_x();
        j["logoShiftTolY"] = x.get_logo_shift_tol_y();
        j["lst_roiTools"] = x.get_lst_roi_tools();
        j["lst_QR_tools"] = x.get_lst_qr_tools();
        j["lst_Group_printCheck_tools"] = x.get_lst_group_print_check_tools();
        j["lst_Datecode_tools"] = x.get_lst_datecode_tools();
        j["lst_WeekCode_tools"] = x.get_lst_week_code_tools();
        j["lst_Mask_tools"] = x.get_lst_mask_tools();
        j["lst_Fixture_tools"] = x.get_lst_fixture_tools();
        j["lst_CROI_tool"] = x.get_lst_croi_tool();
        j["lst_BoundaryGap_tools"] = x.get_lst_boundary_gap_tools();
        j["lst_GrayPresence_tools"] = x.get_lst_gray_presence_tools();
        j["AppliedToolCnt"] = x.get_applied_tool_cnt();
    }
}
