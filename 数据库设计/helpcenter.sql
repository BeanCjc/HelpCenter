/*
 Navicat Premium Data Transfer

 Source Server         : mysql57
 Source Server Type    : MySQL
 Source Server Version : 50723
 Source Host           : localhost:3306
 Source Schema         : helpcenter

 Target Server Type    : MySQL
 Target Server Version : 50723
 File Encoding         : 65001

 Date: 30/10/2018 13:48:00
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for dept
-- ----------------------------
DROP TABLE IF EXISTS `dept`;
CREATE TABLE `dept`  (
  `deptId` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `deptNO` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `deptName` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `deptState` int(1) NULL DEFAULT NULL,
  `preDeptId` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `crUsrId` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `crdt` datetime(0) NULL DEFAULT NULL,
  `updt` datetime(0) NULL DEFAULT NULL,
  `upUsrId` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of dept
-- ----------------------------
INSERT INTO `dept` VALUES ('72c30798-dbe9-11e8-9569-1c1b0d77fd25', '1', '轩亚(福州信息技术有限公司)', 1, '', '1', '2018-10-23 14:49:29', '2018-10-23 14:49:31', '1');
INSERT INTO `dept` VALUES ('60c5b618-dbe9-11e8-9569-1c1b0d77fd25', '2', '总经办', 1, '72c30798-dbe9-11e8-9569-1c1b0d77fd25', '1', '2018-10-24 10:21:44', '2018-10-29 11:02:35', '');
INSERT INTO `dept` VALUES ('56f2a245-dbe9-11e8-9569-1c1b0d77fd25', '3', '客服部', 1, '72c30798-dbe9-11e8-9569-1c1b0d77fd25', '1', '2018-10-24 10:22:18', '2018-10-24 10:22:20', '1');
INSERT INTO `dept` VALUES ('4268dc16-dbe9-11e8-9569-1c1b0d77fd25', '4', '客服一部', 1, '56f2a245-dbe9-11e8-9569-1c1b0d77fd25', '1', '2018-10-24 10:42:42', '2018-10-24 11:11:31', '');
INSERT INTO `dept` VALUES ('78cb860f-086e-430d-a9fd-12b1ca53b9a9', '5', '备机维修部', 1, '56f2a245-dbe9-11e8-9569-1c1b0d77fd25', '', '2018-10-24 10:59:31', '2018-10-24 10:59:31', '');
INSERT INTO `dept` VALUES ('aa719ed3-2d6c-4da0-a0ec-b1a31fa91223', '6', '客服助理', 1, '56f2a245-dbe9-11e8-9569-1c1b0d77fd25', '', '2018-10-29 09:30:30', '2018-10-29 09:30:30', '');
INSERT INTO `dept` VALUES ('33cce0a3-a063-4ef1-b724-c19b301a26e8', '7', '研发部', 1, '72c30798-dbe9-11e8-9569-1c1b0d77fd25', '', '2018-10-29 10:56:57', '2018-10-29 10:56:57', '');
INSERT INTO `dept` VALUES ('e1cc38d7-8ac7-4536-8de6-129230c350be', '8', '财务部', 1, '72c30798-dbe9-11e8-9569-1c1b0d77fd25', '', '2018-10-29 11:05:15', '2018-10-29 11:05:15', '');
INSERT INTO `dept` VALUES ('cc366635-43ae-4245-a08d-6e1fcf5f46a5', '9', '销售部', 1, '72c30798-dbe9-11e8-9569-1c1b0d77fd25', '', '2018-10-29 16:54:17', '2018-10-29 16:54:20', '');

-- ----------------------------
-- Table structure for deptaccount
-- ----------------------------
DROP TABLE IF EXISTS `deptaccount`;
CREATE TABLE `deptaccount`  (
  `deptAccountId` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `deptAccount` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `deptPsw` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `deptId` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `crUsrId` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `crdt` datetime(0) NULL DEFAULT NULL,
  `updt` datetime(0) NULL DEFAULT NULL,
  `upUsrId` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of deptaccount
-- ----------------------------
INSERT INTO `deptaccount` VALUES ('777f74e7-dbea-11e8-9569-1c1b0d77fd25', '1', '1', '1', '1', '2018-10-23 14:52:10', '2018-10-23 14:52:12', '1');
INSERT INTO `deptaccount` VALUES ('b6394e4f-85ba-4719-82c6-311c13dd47e5', '5', '5', '78cb860f-086e-430d-a9fd-12b1ca53b9a9', '', '2018-10-24 10:59:31', '2018-10-24 10:59:31', '');
INSERT INTO `deptaccount` VALUES ('5c492c91-f04c-4e5f-b320-7ce434525a4c', 'kefubu', 'kefubu', 'aa719ed3-2d6c-4da0-a0ec-b1a31fa91223', '', '2018-10-29 09:30:30', '2018-10-29 09:30:30', '');
INSERT INTO `deptaccount` VALUES ('48c3ca34-4b04-44b3-9537-3203b22b671a', 'rlxzb', 'rlxzb', '33cce0a3-a063-4ef1-b724-c19b301a26e8', '', '2018-10-29 10:56:57', '2018-10-29 10:56:57', '');

-- ----------------------------
-- Table structure for docdeptrole
-- ----------------------------
DROP TABLE IF EXISTS `docdeptrole`;
CREATE TABLE `docdeptrole`  (
  `docDeptRoleId` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `docId` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `deptId` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `docDeptState` int(11) NULL DEFAULT NULL,
  `crdt` datetime(0) NULL DEFAULT NULL,
  `crUsrId` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `updt` datetime(0) NULL DEFAULT NULL,
  `upUsrId` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of docdeptrole
-- ----------------------------
INSERT INTO `docdeptrole` VALUES ('1', '6448125c-28e7-420e-80f0-dcb19249f146', '1', 1, '2018-10-28 23:16:24', '1', '2018-10-28 23:16:28', '1');

-- ----------------------------
-- Table structure for doctypeconfig
-- ----------------------------
DROP TABLE IF EXISTS `doctypeconfig`;
CREATE TABLE `doctypeconfig`  (
  `docTypeId` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `docTypeNum` int(11) NULL DEFAULT NULL,
  `docPreTypeId` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `docTypeName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `docTypeDeptId` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `docTypeState` int(11) NULL DEFAULT NULL,
  `crdt` datetime(0) NULL DEFAULT NULL,
  `crUsrId` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `updt` datetime(0) NULL DEFAULT NULL,
  `upUsrId` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of doctypeconfig
-- ----------------------------
INSERT INTO `doctypeconfig` VALUES ('a29853da-dbea-11e8-9569-1c1b0d77fd25', 9, '', '其它', '72c30798-dbe9-11e8-9569-1c1b0d77fd25', 1, '2018-10-24 18:10:39', '1', '2018-10-29 11:23:43', '');
INSERT INTO `doctypeconfig` VALUES ('b94dfcfa-39ba-4634-9f1b-5ae97937fd67', 10, '1', '商超', '72c30798-dbe9-11e8-9569-1c1b0d77fd25', 1, '2018-10-29 11:20:33', '', '2018-10-29 11:20:33', '');

-- ----------------------------
-- Table structure for docusrrole
-- ----------------------------
DROP TABLE IF EXISTS `docusrrole`;
CREATE TABLE `docusrrole`  (
  `docRoleId` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `docId` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `roleId` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `docRoleState` int(11) NULL DEFAULT NULL,
  `crdt` datetime(0) NULL DEFAULT NULL,
  `crUsrId` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `updt` datetime(0) NULL DEFAULT NULL,
  `upUsrId` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of docusrrole
-- ----------------------------
INSERT INTO `docusrrole` VALUES ('9e8c8c91-a43d-4c9c-8c7d-26d9ca0fd266', '6448125c-28e7-420e-80f0-dcb19249f146', '4b5df8d4-e422-48b4-8138-7fd6806dd214', 1, '2018-10-29 03:14:01', '', '2018-10-29 03:14:01', '');

-- ----------------------------
-- Table structure for helpdoc
-- ----------------------------
DROP TABLE IF EXISTS `helpdoc`;
CREATE TABLE `helpdoc`  (
  `docId` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `crdt` datetime(0) NULL DEFAULT NULL,
  `updt` datetime(0) NULL DEFAULT NULL,
  `docTypeId` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `docTitle` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `docContent` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `docFullText` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `docNum` int(11) NULL DEFAULT NULL,
  `docDeptId` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `docCount` int(11) NULL DEFAULT NULL,
  `crUsrId` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `upUsrId` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `docState` int(11) NULL DEFAULT NULL,
  FULLTEXT INDEX `docTitle`(`docTitle`, `docFullText`) WITH PARSER `ngram`
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of helpdoc
-- ----------------------------
INSERT INTO `helpdoc` VALUES ('6448125c-28e7-420e-80f0-dcb19249f146', '2018-10-27 01:03:13', '2018-10-27 01:03:13', 'a29853da-dbea-11e8-9569-1c1b0d77fd25', '哈哈哈这不是哈哈哈哈哈哈哈哈', '《神探蒲松龄之兰若仙踪》今日公布全新剧照以及电影海报，由成龙大叔饰演的“神探大侠”蒲松龄顽皮之中不失豪气，一起来欣赏一下。电影《神探蒲松龄之兰若仙踪》讲述了蒲松龄在屡破奇案、收妖扬善过程中经历的一系列合家欢奇幻故事。', '《神探蒲松龄之兰若仙踪》今日公布全新剧照以及电影海报，由成龙大叔饰演的“神探大侠”蒲松龄顽皮之中不失豪气，一起来欣赏一下。电影《神探蒲松龄之兰若仙踪》讲述了蒲松龄在屡破奇案、收妖扬善过程中经历的一系列合家欢奇幻故事。', 1, '72c30798-dbe9-11e8-9569-1c1b0d77fd25', 0, '', '', 1);
INSERT INTO `helpdoc` VALUES ('f47435de-2883-43da-b894-859130d63099', '2018-10-27 01:05:29', '2018-10-27 01:05:29', 'a29853da-dbea-11e8-9569-1c1b0d77fd25', '谷歌“夜视”拍照功能提前解禁 低光拍照效果提升非常明显', '谷歌展示了iPhone XS及带有夜视功能的Pixel 3智能手机拍摄的低光照片比较，并且后一款设备拍摄的照片更加明亮。谷歌表示，Night Sight将于下个月提供给Pixel智能手机，但XDA者论坛成员设法让该功能提前工作，The Verge的Vlad Savov在Pixel 3 XL上测试了预发布的软件。效果非常显着', '谷歌展示了iPhone XS及带有夜视功能的Pixel 3智能手机拍摄的低光照片比较，并且后一款设备拍摄的照片更加明亮。谷歌表示，Night Sight将于下个月提供给Pixel智能手机，但XDA者论坛成员设法让该功能提前工作，The Verge的Vlad Savov在Pixel 3 XL上测试了预发布的软件。效果非常显着', 2, '60c5b618-dbe9-11e8-9569-1c1b0d77fd25', 0, '', '', 1);
INSERT INTO `helpdoc` VALUES ('bb6312f5-88d2-46f2-af65-3982b31cccba', '2018-10-27 01:52:34', '2018-10-27 01:52:34', 'a29853da-dbea-11e8-9569-1c1b0d77fd25', '双11预售： Apple 苹果 iPhone XS Max 智能手机 64GB 8388元包邮', 'iPhone XS Max是苹果2018年秋季发布的大屏旗舰手机，共有深空灰，金色，银色三种配色，前后双玻璃造型，中框为不锈钢材质。其采用6.5寸 OLED Super Retina全面屏，分辨率2688 x 1242，像素密度458ppi，继续搭载3D Touch，支持DCI-P3广色域，整机三围157.5x77.4x7.7mm，重量208g。', 'iPhone XS Max是苹果2018年秋季发布的大屏旗舰手机，共有深空灰，金色，银色三种配色，前后双玻璃造型，中框为不锈钢材质。其采用6.5寸 OLED Super Retina全面屏，分辨率2688 x 1242，像素密度458ppi，继续搭载3D Touch，支持DCI-P3广色域，整机三围157.5x77.4x7.7mm，重量208g。', 3, '60c5b618-dbe9-11e8-9569-1c1b0d77fd25', 2, '', '', 1);

-- ----------------------------
-- Table structure for role
-- ----------------------------
DROP TABLE IF EXISTS `role`;
CREATE TABLE `role`  (
  `roleId` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `roleName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `roleState` int(11) NULL DEFAULT NULL,
  `roleRemark` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `crdt` datetime(0) NULL DEFAULT NULL,
  `crUsrId` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `updt` datetime(0) NULL DEFAULT NULL,
  `upUsrId` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of role
-- ----------------------------
INSERT INTO `role` VALUES ('4b5df8d4-e422-48b4-8138-7fd6806dd214', 'TEST角色', 0, '说明', '2018-10-24 17:42:12', '', '2018-10-29 15:14:11', '');
INSERT INTO `role` VALUES ('5e99f3d2-5f07-41d1-b76b-8f45cb6c0373', '测试角色', 1, '这里是备注', '2018-10-29 15:12:52', '', '2018-10-29 15:12:52', '');

-- ----------------------------
-- Table structure for rolesysmodule
-- ----------------------------
DROP TABLE IF EXISTS `rolesysmodule`;
CREATE TABLE `rolesysmodule`  (
  `roleSysModuleId` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `sysModuleId` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `roleId` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `roleSysModuleState` int(11) NULL DEFAULT NULL,
  `crdt` datetime(0) NULL DEFAULT NULL,
  `crUsrId` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `updt` datetime(0) NULL DEFAULT NULL,
  `upUsrId` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for sys_dict
-- ----------------------------
DROP TABLE IF EXISTS `sys_dict`;
CREATE TABLE `sys_dict`  (
  `dictId` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `dictTypeCode` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `dictTypeName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `dictCode` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `dictName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `dictState` int(11) NULL DEFAULT NULL,
  `dictNum` int(11) NULL DEFAULT NULL,
  `dictFBH` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `dictRemark` varchar(1000) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `crdt` datetime(0) NULL DEFAULT NULL,
  `crUsrId` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `updt` datetime(0) NULL DEFAULT NULL,
  `upUsrId` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for sys_module
-- ----------------------------
DROP TABLE IF EXISTS `sys_module`;
CREATE TABLE `sys_module`  (
  `moduleId` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `moduleName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `moduleImgPath` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `moduleUrlPath` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `preModuleId` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `moduleRemark` varchar(1000) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `moduleState` int(11) NULL DEFAULT NULL,
  `crdt` datetime(0) NULL DEFAULT NULL,
  `crUsrId` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `updt` datetime(0) NULL DEFAULT NULL,
  `upUsrId` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_module
-- ----------------------------
INSERT INTO `sys_module` VALUES ('7a3f3672-0af5-48d5-8fa3-f37af234a458', '帮助文档', '', '', '0732ab68-2a56-4584-85eb-280c08c986ac', '', 1, '2018-10-29 15:29:06', '', '2018-10-29 15:29:06', '');
INSERT INTO `sys_module` VALUES ('f2f53411-fed9-4706-85f5-b41804deb5a7', '帮助文档详情', '', '', '7a3f3672-0af5-48d5-8fa3-f37af234a458', '', 1, '2018-10-29 15:29:48', '', '2018-10-29 15:29:48', '');
INSERT INTO `sys_module` VALUES ('617f5923-8820-4d12-87eb-8146ccee1d11', '部门管理', '', '', '0732ab68-2a56-4584-85eb-280c08c986ac', '', 1, '2018-10-29 15:31:05', '', '2018-10-29 15:31:05', '');
INSERT INTO `sys_module` VALUES ('33357699-59f6-4160-a351-26137390d7b4', '部门人员管理', '', '', '617f5923-8820-4d12-87eb-8146ccee1d11', '', 1, '2018-10-29 15:31:22', '', '2018-10-29 15:31:22', '');
INSERT INTO `sys_module` VALUES ('14c470fa-11ad-492b-aca5-ef0946bb84b5', '文档管理', '', '', '0732ab68-2a56-4584-85eb-280c08c986ac', '', 1, '2018-10-29 15:31:45', '', '2018-10-29 15:31:45', '');
INSERT INTO `sys_module` VALUES ('b10b760d-f96e-4233-85ac-16fd7e56ec3d', '帮助文档分类设置', '', '', '14c470fa-11ad-492b-aca5-ef0946bb84b5', '', 1, '2018-10-29 15:32:02', '', '2018-10-29 15:32:02', '');
INSERT INTO `sys_module` VALUES ('91ed3071-cb5a-4a05-ab15-bf23b023849d', '帮助文档列表', '', '', '14c470fa-11ad-492b-aca5-ef0946bb84b5', '', 1, '2018-10-29 15:32:15', '', '2018-10-29 15:32:15', '');
INSERT INTO `sys_module` VALUES ('7f1739ba-bab3-4eec-8db0-3108c06152e3', '帮助文档详情修改/新增', '', '', '91ed3071-cb5a-4a05-ab15-bf23b023849d', '', 1, '2018-10-29 15:32:58', '', '2018-10-29 15:32:58', '');
INSERT INTO `sys_module` VALUES ('83bd6bcf-64ec-490a-a46c-34987bfb50c1', '账户管理', '', '', '0732ab68-2a56-4584-85eb-280c08c986ac', '', 1, '2018-10-29 15:33:08', '', '2018-10-29 15:33:08', '');
INSERT INTO `sys_module` VALUES ('90e86163-ea78-4bc3-8132-4e0dc02f710a', '用户管理', '', '', '83bd6bcf-64ec-490a-a46c-34987bfb50c1', '', 1, '2018-10-29 15:33:17', '', '2018-10-29 15:33:17', '');
INSERT INTO `sys_module` VALUES ('28d09ec2-f748-45b7-9c5d-eac4224fac2f', '角色管理', '', '', '83bd6bcf-64ec-490a-a46c-34987bfb50c1', '', 1, '2018-10-29 15:33:25', '', '2018-10-29 15:33:25', '');
INSERT INTO `sys_module` VALUES ('0d170859-6d1b-4788-9000-fcc974ad7bd2', '模块授权管理', '', '', '28d09ec2-f748-45b7-9c5d-eac4224fac2f', '', 1, '2018-10-29 15:33:33', '', '2018-10-29 15:33:33', '');
INSERT INTO `sys_module` VALUES ('0732ab68-2a56-4584-85eb-280c08c986ac', '帮助中心', '', '', '', '', 1, '2018-10-29 15:34:20', '', '2018-10-29 15:34:20', '');
INSERT INTO `sys_module` VALUES ('1a6b0cfa-24bf-4e86-beb6-f1550212d559', '登陆注册', '', '', '0732ab68-2a56-4584-85eb-280c08c986ac', '', 1, '2018-10-29 15:34:46', '', '2018-10-29 15:34:46', '');

-- ----------------------------
-- Table structure for usr
-- ----------------------------
DROP TABLE IF EXISTS `usr`;
CREATE TABLE `usr`  (
  `usrId` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `usrPhoneNum` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `usrAccount` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `usrPsw` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `usrName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `usrDeptId` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `usrVerifyState` int(11) NULL DEFAULT NULL,
  `usrType` int(11) NULL DEFAULT NULL,
  `usrState` int(11) NULL DEFAULT NULL,
  `crdt` datetime(0) NULL DEFAULT NULL,
  `crUsrId` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `updt` datetime(0) NULL DEFAULT NULL,
  `upUsrId` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of usr
-- ----------------------------
INSERT INTO `usr` VALUES ('rrr30798-dbe9-11e8-9569-1c1b0d77fd25', '13333333333', '3', '3', '大明', '60c5b618-dbe9-11e8-9569-1c1b0d77fd25', 1, 2, 1, '2018-10-22 10:09:09', '1', '2018-10-22 10:09:16', '1');
INSERT INTO `usr` VALUES ('3e2a9f04-48b7-4eda-8c61-502a4753dae8', '18888888888', 'string', 'string', '机智的游客', '56f2a245-dbe9-11e8-9569-1c1b0d77fd25', 1, 3, 1, '2018-10-23 11:51:23', '', '2018-10-23 11:51:23', '');
INSERT INTO `usr` VALUES ('32911113-da9d-4cf7-bfc0-708589e94962', '15999999999', '222', '333', '客服部', '56f2a245-dbe9-11e8-9569-1c1b0d77fd25', 1, 1, 1, '2018-10-24 15:14:10', '', '2018-10-29 14:47:09', '');
INSERT INTO `usr` VALUES ('3a8cf495-db1a-11e8-9569-1c1b0d77fd25', '18666666666', 'yanfabu', 'yanfabu', '研发部', '33cce0a3-a063-4ef1-b724-c19b301a26e8', 1, 1, 1, '2018-10-29 09:30:30', '', '2018-10-29 09:30:30', '');
INSERT INTO `usr` VALUES ('4e7dce1b-db26-11e8-9569-1c1b0d77fd25', '18977777777', 'caiwubu', 'caiwubu', '财务部', 'e1cc38d7-8ac7-4536-8de6-129230c350be', 0, 1, 1, '2018-10-29 10:56:57', '', '2018-10-29 10:56:57', '');

-- ----------------------------
-- Table structure for usrrole
-- ----------------------------
DROP TABLE IF EXISTS `usrrole`;
CREATE TABLE `usrrole`  (
  `usrRoleId` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `usrId` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `roleId` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `usrRoleState` int(11) NULL DEFAULT NULL,
  `crdt` datetime(0) NULL DEFAULT NULL,
  `crUsrId` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `updt` datetime(0) NULL DEFAULT NULL,
  `upUsrId` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of usrrole
-- ----------------------------
INSERT INTO `usrrole` VALUES ('1', '1', '4b5df8d4-e422-48b4-8138-7fd6806dd214', 1, '2018-10-28 19:08:39', '1', '2018-10-28 19:08:44', '1');

-- ----------------------------
-- View structure for doc_visitor
-- ----------------------------
DROP VIEW IF EXISTS `doc_visitor`;
CREATE ALGORITHM = UNDEFINED DEFINER = `root`@`localhost` SQL SECURITY DEFINER VIEW `doc_visitor` AS select `doc`.`docid` AS `docid`,`doc`.`docTypeId` AS `docTypeId`,`doc`.`docTitle` AS `docTitle`,`doc`.`docContent` AS `docContent`,`doc`.`docFullText` AS `docFullText`,`doc`.`docNum` AS `docNum`,`doc`.`docCount` AS `docCount`,`doc`.`crdt` AS `crdt`,`doc`.`updt` AS `updt`,`helpcenter`.`doctypeconfig`.`docTypeName` AS `docTypeName` from (((select `helpcenter`.`helpdoc`.`docId` AS `docid`,`helpcenter`.`helpdoc`.`docTypeId` AS `docTypeId`,`helpcenter`.`helpdoc`.`docTitle` AS `docTitle`,`helpcenter`.`helpdoc`.`docContent` AS `docContent`,`helpcenter`.`helpdoc`.`docFullText` AS `docFullText`,`helpcenter`.`helpdoc`.`docNum` AS `docNum`,`helpcenter`.`helpdoc`.`docCount` AS `docCount`,`helpcenter`.`helpdoc`.`crdt` AS `crdt`,`helpcenter`.`helpdoc`.`updt` AS `updt` from (((select `helpcenter`.`docusrrole`.`docId` AS `docid` from `helpcenter`.`docusrrole` where (`helpcenter`.`docusrrole`.`roleId` = '4b5df8d4-e422-48b4-8138-7fd6806dd214'))) `dur` join `helpcenter`.`helpdoc` on(((`dur`.`docid` = `helpcenter`.`helpdoc`.`docId`) and (`helpcenter`.`helpdoc`.`docState` <> 0)))))) `doc` left join `helpcenter`.`doctypeconfig` on((`doc`.`docTypeId` = `helpcenter`.`doctypeconfig`.`docTypeId`)));

-- ----------------------------
-- Function structure for FN_DEPT_CHECKCHILD
-- ----------------------------
DROP FUNCTION IF EXISTS `FN_DEPT_CHECKCHILD`;
delimiter ;;
CREATE DEFINER=`root`@`localhost` FUNCTION `FN_DEPT_CHECKCHILD`( deptid VARCHAR(64) ) RETURNS int(11)
BEGIN
DECLARE
	cnt INT;
SELECT
	count(1) INTO cnt 
FROM
	dept 
WHERE
	preDeptId = deptid;
RETURN cnt;
END
;;
delimiter ;

-- ----------------------------
-- Function structure for FN_DOCTYPECONFIG_DOCTYPENAME
-- ----------------------------
DROP FUNCTION IF EXISTS `FN_DOCTYPECONFIG_DOCTYPENAME`;
delimiter ;;
CREATE DEFINER=`root`@`localhost` FUNCTION `FN_DOCTYPECONFIG_DOCTYPENAME`( _docTypeId VARCHAR ( 64 ) ) RETURNS varchar(64) CHARSET utf8
BEGIN
DECLARE
	returnValue VARCHAR ( 64 );

SET @_docTypeId = _docTypeId;
SELECT
	docTypeName INTO returnValue 
FROM
	doctypeconfig 
WHERE
	docTypeId = @_docTypeId;
RETURN returnValue;

END
;;
delimiter ;

-- ----------------------------
-- Function structure for FN_ROLE_ROLENAME
-- ----------------------------
DROP FUNCTION IF EXISTS `FN_ROLE_ROLENAME`;
delimiter ;;
CREATE DEFINER=`root`@`localhost` FUNCTION `FN_ROLE_ROLENAME`( _roleId VARCHAR ( 64 ) ) RETURNS varchar(64) CHARSET utf8
BEGIN
DECLARE
	returnValue VARCHAR ( 64 );

SET @_roleId = _roleId;
SELECT
	roleName INTO returnValue 
FROM
	role 
WHERE
	roleId = @_roleId;
RETURN returnValue;

END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_ADD_DEPT_ACCOUNT
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_ADD_DEPT_ACCOUNT`;
delimiter ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `SP_ADD_DEPT_ACCOUNT`(
IN _deptId VARCHAR ( 64 ),
IN _deptNO VARCHAR ( 64 ),
IN _deptName VARCHAR ( 64 ),
IN _deptState INT,
IN _preDeptId VARCHAR ( 64 ),
IN _deptAccountId VARCHAR ( 64 ),
IN _deptAccount VARCHAR ( 64 ),
IN _deptPsw VARCHAR ( 64 ),
IN _crUsrId VARCHAR ( 64 ),
IN _crdt datetime,
IN _updt datetime,
IN _upUsrId VARCHAR ( 64 ),
OUT isErr INT 
)
BEGIN
DECLARE
	t_error INTEGER DEFAULT 0;
DECLARE
CONTINUE HANDLER FOR SQLEXCEPTION 
	SET t_error = 1;
START TRANSACTION;
INSERT INTO `helpcenter`.`dept` ( `deptId`, `deptNO`, `deptName`, `deptState`, `preDeptId`, `crUsrId`, `crdt`, `updt`, `upUsrId` )
VALUES
	( _deptId, _deptNO, _deptName, _deptState, _preDeptId, _crUsrId, _crdt, _updt, _upUsrId );
INSERT INTO `helpcenter`.`deptaccount` ( `deptAccountId`, `deptAccount`, `deptPsw`, `deptId`, `crUsrId`, `crdt`, `updt`, `upUsrId` )
VALUES
	( _deptAccountId, _deptAccount, _deptPsw, _deptId, _crUsrId, _crdt, _updt, _upUsrId );
INSERT INTO usr ( usrId, usrPhoneNum, usrAccount, usrPsw, usrName, usrDeptId, usrVerifyState, usrType, usrState, crdt, crUsrId, updt, upUsrId )
VALUES
	(
	UUID( ),
	_deptAccount,
	_deptAccount,
	_deptPsw,
	_deptName,
	_deptId,
	0,
	2,
	3,
	_crdt,
	_crUsrId,
	_updt,
	_upUsrId 
	);
IF
	t_error = 1 THEN
	ROLLBACK;
ELSE COMMIT;

END IF;

SET isErr = t_error;

END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_PAGER
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_PAGER`;
delimiter ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `SP_PAGER`(
	#输入参数
	_fields VARCHAR(2000), #要查询的字段，用逗号(,)分隔
	_tables TEXT,  #要查询的表
	_where VARCHAR(2000),   #查询条件
	_orderby VARCHAR(200),  #排序规则
	_pageindex INT,  #查询页码
	_pageSize INT,   #每页记录数
	_sumfields VARCHAR(200),#求和字段
	#输出参数
	OUT _totalcount INT,  #总记录数
	OUT _pagecount INT,    #总页数
	OUT _sumResult VARCHAR(2000)#求和结果
)
BEGIN
	#140529-xxj-分页存储过程
	#计算起始行号
	SET @startRow = _pageSize * (_pageIndex - 1);
	SET @pageSize = _pageSize;
	SET @rowindex = 0; #行号
 
	#合并字符串
	SET @strsql = CONCAT(
		#'select sql_calc_found_rows  @rowindex:=@rowindex+1 as rownumber,' #记录行号
		'select sql_calc_found_rows '
		,_fields
		,' from '
		,_tables
		,CASE IFNULL(_where, '') WHEN '' THEN '' ELSE CONCAT(' where ', _where) END
		,CASE IFNULL(_orderby, '') WHEN '' THEN '' ELSE CONCAT(' order by ', _orderby) END
	  ,' limit ' 
		,@startRow
		,',' 
		,@pageSize
	);
 
	PREPARE strsql FROM @strsql;#定义预处理语句 
	EXECUTE strsql;							#执行预处理语句 
	DEALLOCATE PREPARE strsql;	#删除定义 
	#通过 sql_calc_found_rows 记录没有使用 limit 语句的记录，使用 found_rows() 获取行数
	SET _totalcount = FOUND_ROWS();
 
	#计算总页数
	IF (_totalcount <= _pageSize) THEN
		SET _pagecount = 1;
	ELSE
		SET _pagecount = (_totalcount + _pageSize-1)/_pageSize;
	END IF;
 
	#计算求和字段
	IF (IFNULL(_sumfields, '') <> '') THEN
		#序列sum结果
		SET @sumCols = CONCAT (
			'CONCAT_WS(\',\','
			,'SUM('
			,REPLACE(_sumfields,',','),SUM(')
			,'))');
		#拼接字符串
		SET @sumsql = CONCAT(
			'select '
			,@sumCols
			,' INTO @sumResult from '
			,_tables
			,CASE IFNULL(_where, '') WHEN '' THEN '' ELSE CONCAT(' where ', _where) END
			,';'
		);
		#select @sumsql;
		PREPARE sumsql FROM @sumsql;#定义预处理语句 
		EXECUTE sumsql;	
		SET _sumResult = @sumResult;						#执行预处理语句 
		DEALLOCATE PREPARE sumsql;	#删除定义 
 
	END IF;
 
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_PAGER_DEPT_DEPTNAME
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_PAGER_DEPT_DEPTNAME`;
delimiter ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `SP_PAGER_DEPT_DEPTNAME`(#输入参数
	_fields VARCHAR ( 2000 ),#要查询的字段，用逗号(,)分隔
	_tables VARCHAR ( 255 ),#要查询的表
	_where VARCHAR ( 2000 ),#查询条件
	_orderby VARCHAR ( 200 ),#排序规则
	_pageindex INT,#查询页码
	_pageSize INT,#每页记录数
	_paramsDeptName VARCHAR ( 255 ),#查询条件
	_sumfields VARCHAR ( 200 ),#求和字段
#输出参数
	OUT _totalcount INT,#总记录数
	OUT _pagecount INT,#总页数
	OUT _sumResult VARCHAR ( 2000 ) #求和结果
	
	)
BEGIN#140529-xxj-分页存储过程
#计算起始行号
	
	SET @p = _paramsDeptName;
	
	SET @startRow = _pageSize * ( _pageIndex - 1 );
	
	SET @pageSize = _pageSize;
	
	SET @rowindex = 0;#行号
#合并字符串
	
	SET @strsql = CONCAT(#'select sql_calc_found_rows  @rowindex:=@rowindex+1 as rownumber,' #记录行号
		'select sql_calc_found_rows ',
		_fields,
		' from ',
		_tables,
		CASE
				IFNULL( _where, '' ) 
				WHEN '' THEN
				'' ELSE CONCAT( ' where ', _where, ' and deptName like concat(\'%\',?,\'%\')' ) 
			END,
		CASE
				IFNULL( _orderby, '' ) 
				WHEN '' THEN
				'' ELSE CONCAT( ' order by ', _orderby ) 
			END,
			' limit ',
			@startRow,
			',',
			@pageSize 
		);
	PREPARE strsql 
	FROM
		@strsql;#定义预处理语句
	EXECUTE strsql USING @p;#执行预处理语句
	DEALLOCATE PREPARE strsql;#删除定义
#通过 sql_calc_found_rows 记录没有使用 limit 语句的记录，使用 found_rows() 获取行数
	
	SET _totalcount = FOUND_ROWS( );#计算总页数
	IF
		( _totalcount <= _pageSize ) THEN
			
			SET _pagecount = 1;
		ELSE 
			SET _pagecount = ( _totalcount + _pageSize - 1 ) / _pageSize;
		
	END IF;#计算求和字段
	IF
		( IFNULL( _sumfields, '' ) <> '' ) THEN#序列sum结果
			
			SET @sumCols = CONCAT ( 'CONCAT_WS(\',\',', 'SUM(', REPLACE ( _sumfields, ',', '),SUM(' ), '))' );#拼接字符串
		
		SET @sumsql = CONCAT(
			'select ',
			@sumCols,
			' INTO @sumResult from ',
			_tables,
			CASE
					IFNULL( _where, '' ) 
					WHEN '' THEN
					'' ELSE CONCAT( ' where ', _where, ' and deptName like concat(\'%\',?,\'%\')' ) 
				END,
				';' 
		);#select @sumsql;
		PREPARE sumsql 
		FROM
			@sumsql;#定义预处理语句
		EXECUTE sumsql USING @p;
		
		SET _sumResult = @sumResult;#执行预处理语句
		DEALLOCATE PREPARE sumsql;#删除定义
		
	END IF;

END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_PAGER_DOCTYPECONFIG_DEPTID
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_PAGER_DOCTYPECONFIG_DEPTID`;
delimiter ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `SP_PAGER_DOCTYPECONFIG_DEPTID`(#输入参数
	_fields VARCHAR ( 2000 ),#要查询的字段，用逗号(,)分隔
	_tables VARCHAR ( 255 ),#要查询的表
	_where VARCHAR ( 2000 ),#查询条件
	_orderby VARCHAR ( 200 ),#排序规则
	_pageindex INT,#查询页码
	_pageSize INT,#每页记录数
	_paramsDeptID VARCHAR ( 255 ),#查询条件
	_sumfields VARCHAR ( 200 ),#求和字段
#输出参数
	OUT _totalcount INT,#总记录数
	OUT _pagecount INT,#总页数
	OUT _sumResult VARCHAR ( 2000 ) #求和结果
	
	)
BEGIN#140529-xxj-分页存储过程
#计算起始行号
	
	SET @p = _paramsDeptID;
	
	SET @startRow = _pageSize * ( _pageIndex - 1 );
	
	SET @pageSize = _pageSize;
	
	SET @rowindex = 0;#行号
#合并字符串
	
	SET @strsql = CONCAT(#'select sql_calc_found_rows  @rowindex:=@rowindex+1 as rownumber,' #记录行号
		'select sql_calc_found_rows ',
		_fields,
		' from ',
		_tables,
		CASE
				IFNULL( _where, '' ) 
				WHEN '' THEN
				'' ELSE CONCAT( ' where ', _where, ' and docTypeDeptId = ?' ) 
			END,
		CASE
				IFNULL( _orderby, '' ) 
				WHEN '' THEN
				'' ELSE CONCAT( ' order by ', _orderby ) 
			END,
			' limit ',
			@startRow,
			',',
			@pageSize 
		);
	PREPARE strsql 
	FROM
		@strsql;#定义预处理语句
	EXECUTE strsql USING @p;#执行预处理语句
	DEALLOCATE PREPARE strsql;#删除定义
#通过 sql_calc_found_rows 记录没有使用 limit 语句的记录，使用 found_rows() 获取行数
	
	SET _totalcount = FOUND_ROWS( );#计算总页数
	IF
		( _totalcount <= _pageSize ) THEN
			
			SET _pagecount = 1;
		ELSE 
			SET _pagecount = ( _totalcount + _pageSize - 1 ) / _pageSize;
		
	END IF;#计算求和字段
	IF
		( IFNULL( _sumfields, '' ) <> '' ) THEN#序列sum结果
			
			SET @sumCols = CONCAT ( 'CONCAT_WS(\',\',', 'SUM(', REPLACE ( _sumfields, ',', '),SUM(' ), '))' );#拼接字符串
		
		SET @sumsql = CONCAT(
			'select ',
			@sumCols,
			' INTO @sumResult from ',
			_tables,
			CASE
					IFNULL( _where, '' ) 
					WHEN '' THEN
				'' ELSE CONCAT( ' where ', _where, ' and docTypeDeptId = ?' ) 
				END,
				';' 
		);#select @sumsql;
		PREPARE sumsql 
		FROM
			@sumsql;#定义预处理语句
		EXECUTE sumsql USING @p;
		
		SET _sumResult = @sumResult;#执行预处理语句
		DEALLOCATE PREPARE sumsql;#删除定义
		
	END IF;

END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_PAGER_HELPDOC_DEPTID
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_PAGER_HELPDOC_DEPTID`;
delimiter ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `SP_PAGER_HELPDOC_DEPTID`(#输入参数
	_fields VARCHAR ( 2000 ),#要查询的字段，用逗号(,)分隔
	_tables VARCHAR ( 255 ),#要查询的表
	_where VARCHAR ( 2000 ),#查询条件
	_orderby VARCHAR ( 200 ),#排序规则
	_pageindex INT,#查询页码
	_pageSize INT,#每页记录数
	_paramsDeptID VARCHAR ( 255 ),#查询条件
	_sumfields VARCHAR ( 200 ),#求和字段
#输出参数
	OUT _totalcount INT,#总记录数
	OUT _pagecount INT,#总页数
	OUT _sumResult VARCHAR ( 2000 ) #求和结果
	
	)
BEGIN#140529-xxj-分页存储过程
#计算起始行号
	
	SET @p = _paramsDeptID;
	
	SET @startRow = _pageSize * ( _pageIndex - 1 );
	
	SET @pageSize = _pageSize;
	
	SET @rowindex = 0;#行号
#合并字符串
	
	SET @strsql = CONCAT(#'select sql_calc_found_rows  @rowindex:=@rowindex+1 as rownumber,' #记录行号
		'select sql_calc_found_rows ',
		_fields,
		' from ',
		_tables,
		CASE
				IFNULL( _where, '' ) 
				WHEN '' THEN
				'' ELSE CONCAT( ' where ', _where, ' and docDeptId = ?' ) 
			END,
		CASE
				IFNULL( _orderby, '' ) 
				WHEN '' THEN
				'' ELSE CONCAT( ' order by ', _orderby ) 
			END,
			' limit ',
			@startRow,
			',',
			@pageSize 
		);
	PREPARE strsql 
	FROM
		@strsql;#定义预处理语句
	EXECUTE strsql USING @p;#执行预处理语句
	DEALLOCATE PREPARE strsql;#删除定义
#通过 sql_calc_found_rows 记录没有使用 limit 语句的记录，使用 found_rows() 获取行数
	
	SET _totalcount = FOUND_ROWS( );#计算总页数
	IF
		( _totalcount <= _pageSize ) THEN
			
			SET _pagecount = 1;
		ELSE 
			SET _pagecount = ( _totalcount + _pageSize - 1 ) / _pageSize;
		
	END IF;#计算求和字段
	IF
		( IFNULL( _sumfields, '' ) <> '' ) THEN#序列sum结果
			
			SET @sumCols = CONCAT ( 'CONCAT_WS(\',\',', 'SUM(', REPLACE ( _sumfields, ',', '),SUM(' ), '))' );#拼接字符串
		
		SET @sumsql = CONCAT(
			'select ',
			@sumCols,
			' INTO @sumResult from ',
			_tables,
			CASE
					IFNULL( _where, '' ) 
					WHEN '' THEN
				'' ELSE CONCAT( ' where ', _where, ' and docDeptId = ?' ) 
				END,
				';' 
		);#select @sumsql;
		PREPARE sumsql 
		FROM
			@sumsql;#定义预处理语句
		EXECUTE sumsql USING @p;
		
		SET _sumResult = @sumResult;#执行预处理语句
		DEALLOCATE PREPARE sumsql;#删除定义
		
	END IF;

END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_PAGER_HELPDOC_KEYWORD_LOGIN
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_PAGER_HELPDOC_KEYWORD_LOGIN`;
delimiter ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `SP_PAGER_HELPDOC_KEYWORD_LOGIN`(#输入参数
  _usrId VARCHAR ( 64 ),
	_where VARCHAR ( 2000 ),#查询条件
	_orderby VARCHAR ( 200 ),#排序规则
	_pageindex INT,#查询页码
	_pageSize INT,#每页记录数
	_paramsKeyWord VARCHAR ( 255 ),#查询条件
	_sumfields VARCHAR ( 200 ),#求和字段
#输出参数
	OUT _totalcount INT,#总记录数
	OUT _pagecount INT,#总页数
	OUT _sumResult VARCHAR ( 2000 ) #求和结果
	
	)
BEGIN#140529-xxj-分页存储过程
#计算起始行号
  SET @_usrId = _usrId;
	
	SET @p = _paramsKeyWord;
	
	SET @startRow = _pageSize * ( _pageIndex - 1 );
	
	SET @pageSize = _pageSize;
	
	SET @rowindex = 0;#行号
#合并字符串
	
	SET @strsql = CONCAT(#'select sql_calc_found_rows  @rowindex:=@rowindex+1 as rownumber,' #记录行号
		'select sql_calc_found_rows ',
		'*',
		' from ',
		
	'(SELECT
		_docusrrole.usrid,
		_docusrrole.usrName,
		_docusrrole.usrDeptId,
		_docusrrole.roleId,
		_docusrrole.roleName,
		_docusrrole.docId docUsrRoleDocId,
		docdeptrole.docId docDeptRoleDocId,
		docdeptrole.deptId 
	FROM
		(
		SELECT
			_usrrole.*,
			docusrrole.docId 
		FROM
			(
			SELECT
				_urole.usrid,
				_urole.usrName,
				_urole.usrDeptId,
				role.roleId,
				role.roleName 
			FROM
				(
				SELECT
					_usr.usrid,
					_usr.usrName,
					_usr.usrDeptId,
					usrrole.roleId 
				FROM
/*匹配用户ID，检查用户状态*/
					( SELECT usr.usrid, usr.usrName, usr.usrDeptId FROM usr WHERE usrId = ? AND usr.usrState != 0 ) _usr
					LEFT JOIN usrrole ON _usr.usrId = usrrole.usrId 
					AND usrrole.usrRoleState != 0 
				) _urole /*关联用户的角色关系*/
				LEFT JOIN role ON _urole.roleId = role.roleid /*检查用户对应的角色状态*/
				
				AND role.roleState != 0 
			) _usrrole /*关联角色和文档的关系*/
			LEFT JOIN docusrrole ON _usrrole.roleid = docusrrole.roleId 
		) _docusrrole
		LEFT JOIN docdeptrole ON _docusrrole.usrDeptId = docdeptrole.deptId 
	) _docrole /*除角色和文档关系外，文档所属部门的人员也可见*/
	INNER JOIN helpdoc ON (
/*用户关联角色，角色关联文档*/
		_docrole.docUsrRoleDocId = helpdoc.docId 
		OR /*文档关联分享部门*/
		_docrole.docDeptRoleDocId = helpdoc.docId 
		OR /*文档本身归属部门*/
		helpdoc.docDeptId = _docrole.usrDeptId )',
		CASE
				IFNULL( _where, '' ) 
				WHEN '' THEN
				'' ELSE CONCAT( ' where ', _where, '  AGAINST (?) ' ) 
			END,
		CASE
				IFNULL( _orderby, '' ) 
				WHEN '' THEN
				'' ELSE CONCAT( ' order by ', _orderby ) 
			END,
			' limit ',
			@startRow,
			',',
			@pageSize 
		);
	PREPARE strsql 
	FROM
		@strsql;#定义预处理语句
	EXECUTE strsql USING @_usrId,@p;#执行预处理语句
	DEALLOCATE PREPARE strsql;#删除定义
#通过 sql_calc_found_rows 记录没有使用 limit 语句的记录，使用 found_rows() 获取行数
	
	SET _totalcount = FOUND_ROWS( );#计算总页数
	IF
		( _totalcount <= _pageSize ) THEN
			
			SET _pagecount = 1;
		ELSE 
			SET _pagecount = ( _totalcount + _pageSize - 1 ) / _pageSize;
		
	END IF;#计算求和字段
	IF
		( IFNULL( _sumfields, '' ) <> '' ) THEN#序列sum结果
			
			SET @sumCols = CONCAT ( 'CONCAT_WS(\',\',', 'SUM(', REPLACE ( _sumfields, ',', '),SUM(' ), '))' );#拼接字符串
		
		SET @sumsql = CONCAT(
			'select ',
			@sumCols,
			' INTO @sumResult from ',
			_tables,
			CASE
					IFNULL( _where, '' ) 
					WHEN '' THEN
					'' ELSE CONCAT( ' where ', _where, '  AGAINST (?) ' ) 
				END,
				';' 
		);#select @sumsql;
		PREPARE sumsql 
		FROM
			@sumsql;#定义预处理语句
		EXECUTE sumsql USING @p;
		
		SET _sumResult = @sumResult;#执行预处理语句
		DEALLOCATE PREPARE sumsql;#删除定义
		
	END IF;

END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_PAGER_HELPDOC_KEYWORD_LOGOUT
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_PAGER_HELPDOC_KEYWORD_LOGOUT`;
delimiter ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `SP_PAGER_HELPDOC_KEYWORD_LOGOUT`(#输入参数
	_fields VARCHAR ( 2000 ),#要查询的字段，用逗号(,)分隔
	_tables VARCHAR ( 255 ),#要查询的表
	_where VARCHAR ( 2000 ),#查询条件
	_orderby VARCHAR ( 200 ),#排序规则
	_pageindex INT,#查询页码
	_pageSize INT,#每页记录数
	_paramsKeyWord VARCHAR ( 255 ),#查询条件
	_sumfields VARCHAR ( 200 ),#求和字段
#输出参数
	OUT _totalcount INT,#总记录数
	OUT _pagecount INT,#总页数
	OUT _sumResult VARCHAR ( 2000 ) #求和结果
	
	)
BEGIN#140529-xxj-分页存储过程
#计算起始行号
	
	SET @p = _paramsKeyWord;
	
	SET @startRow = _pageSize * ( _pageIndex - 1 );
	
	SET @pageSize = _pageSize;
	
	SET @rowindex = 0;#行号
#合并字符串
	
	SET @strsql = CONCAT(#'select sql_calc_found_rows  @rowindex:=@rowindex+1 as rownumber,' #记录行号
		'select sql_calc_found_rows ',
		_fields,
		' from ',
		_tables,
		CASE
				IFNULL( _where, '' ) 
				WHEN '' THEN
				'' ELSE CONCAT( ' where ', _where, '  AGAINST (?) ' ) 
			END,
		CASE
				IFNULL( _orderby, '' ) 
				WHEN '' THEN
				'' ELSE CONCAT( ' order by ', _orderby ) 
			END,
			' limit ',
			@startRow,
			',',
			@pageSize 
		);
	PREPARE strsql 
	FROM
		@strsql;#定义预处理语句
	EXECUTE strsql USING @p;#执行预处理语句
	DEALLOCATE PREPARE strsql;#删除定义
#通过 sql_calc_found_rows 记录没有使用 limit 语句的记录，使用 found_rows() 获取行数
	
	SET _totalcount = FOUND_ROWS( );#计算总页数
	IF
		( _totalcount <= _pageSize ) THEN
			
			SET _pagecount = 1;
		ELSE 
			SET _pagecount = ( _totalcount + _pageSize - 1 ) / _pageSize;
		
	END IF;#计算求和字段
	IF
		( IFNULL( _sumfields, '' ) <> '' ) THEN#序列sum结果
			
			SET @sumCols = CONCAT ( 'CONCAT_WS(\',\',', 'SUM(', REPLACE ( _sumfields, ',', '),SUM(' ), '))' );#拼接字符串
		
		SET @sumsql = CONCAT(
			'select ',
			@sumCols,
			' INTO @sumResult from ',
			_tables,
			CASE
					IFNULL( _where, '' ) 
					WHEN '' THEN
					'' ELSE CONCAT( ' where ', _where, '  AGAINST (?) ' ) 
				END,
				';' 
		);#select @sumsql;
		PREPARE sumsql 
		FROM
			@sumsql;#定义预处理语句
		EXECUTE sumsql USING @p;
		
		SET _sumResult = @sumResult;#执行预处理语句
		DEALLOCATE PREPARE sumsql;#删除定义
		
	END IF;

END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_PAGER_USR_DEPTID
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_PAGER_USR_DEPTID`;
delimiter ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `SP_PAGER_USR_DEPTID`(#输入参数
_where VARCHAR ( 2000 ),#查询条件
_orderby VARCHAR ( 200 ),#排序规则
_pageindex INT,#查询页码
_pageSize INT,#每页记录数
_paramsDeptID VARCHAR ( 255 ),#查询条件
_sumfields VARCHAR ( 200 ),#求和字段
#输出参数
OUT _totalcount INT,#总记录数
OUT _pagecount INT,#总页数
OUT _sumResult VARCHAR ( 2000 ) #求和结果

)
BEGIN#140529-xxj-分页存储过程
#计算起始行号

	SET @p = _paramsDeptID;
	
	SET @startRow = _pageSize * ( _pageIndex - 1 );
	
	SET @pageSize = _pageSize;
	
	SET @rowindex = 0;#行号
	set @deptsql=(
		SELECT
		CASE
				
			WHEN
				( _paramsDeptID IS NULL OR _paramsDeptID ='' ) THEN
					
					 '' ELSE 
			 ' AND usr.usrDeptId = ? ' 
		END);#合并字符串#合并字符串
	
	SET @strsql = CONCAT(#'select sql_calc_found_rows  @rowindex:=@rowindex+1 as rownumber,' #记录行号
		'select sql_calc_found_rows ',
		'	usr.crdt,
		usr.usrid,
		usr.usrDeptId,
		usr.usrName,
		usr.usrType,
		usr.usrVerifyState,
		usr.usrAccount,
		dept.deptName ',
		' from ',
		'usr INNER JOIN dept ON usr.usrDeptId = dept.deptId ',
		CASE
				IFNULL( _where, '' ) 
				WHEN '' THEN
				'' ELSE CONCAT( ' where ', _where, @deptsql ) 
			END,
		CASE
				IFNULL( _orderby, '' ) 
				WHEN '' THEN
				'' ELSE CONCAT( ' order by ', _orderby ) 
			END,
			' limit ',
			@startRow,
			',',
			@pageSize 
		);
	PREPARE strsql 
	FROM
		@strsql;#定义预处理语句
		
	#执行预处理语句
		IF ( _paramsDeptID IS NULL OR _paramsDeptID = '' ) THEN
			EXECUTE strsql;
			ELSE
			EXECUTE strsql USING @p;
		END IF;
	
	
	DEALLOCATE PREPARE strsql;#删除定义
#通过 sql_calc_found_rows 记录没有使用 limit 语句的记录，使用 found_rows() 获取行数
	
	SET _totalcount = FOUND_ROWS( );#计算总页数
	IF
		( _totalcount <= _pageSize ) THEN
			
			SET _pagecount = 1;
		ELSE 
			SET _pagecount = ( _totalcount + _pageSize - 1 ) / _pageSize;
		
	END IF;#计算求和字段
	IF
		( IFNULL( _sumfields, '' ) <> '' ) THEN#序列sum结果
			
			SET @sumCols = CONCAT ( 'CONCAT_WS(\',\',', 'SUM(', REPLACE ( _sumfields, ',', '),SUM(' ), '))' );#拼接字符串
		
		SET @sumsql = CONCAT(
			'select ',
			@sumCols,
			' INTO @sumResult from ',
			_tables,
			CASE
					IFNULL( _where, '' ) 
					WHEN '' THEN
					'' ELSE CONCAT( ' where ', _where, @deptsql ) 
				END,
				';' 
		);#select @sumsql;
		PREPARE sumsql 
		FROM
			@sumsql;#定义预处理语句
			
		IF ( _paramsDeptID IS NULL OR _paramsDeptID = '' ) THEN
			EXECUTE sumsql;
			ELSE
			EXECUTE sumsql USING @p;
		END IF;
		
		SET _sumResult = @sumResult;#执行预处理语句
		DEALLOCATE PREPARE sumsql;#删除定义
		
	END IF;
	
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_PAGER_USR_USRNAME
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_PAGER_USR_USRNAME`;
delimiter ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `SP_PAGER_USR_USRNAME`(#输入参数
	_fields VARCHAR ( 2000 ),#要查询的字段，用逗号(,)分隔
	_tables VARCHAR ( 255 ),#要查询的表
	_where VARCHAR ( 2000 ),#查询条件
	_orderby VARCHAR ( 200 ),#排序规则
	_pageindex INT,#查询页码
	_pageSize INT,#每页记录数
	_paramsUsrName VARCHAR ( 255 ),#查询条件
	_sumfields VARCHAR ( 200 ),#求和字段
#输出参数
	OUT _totalcount INT,#总记录数
	OUT _pagecount INT,#总页数
	OUT _sumResult VARCHAR ( 2000 ) #求和结果
	
	)
BEGIN#140529-xxj-分页存储过程
#计算起始行号
	
	SET @p = _paramsUsrName;
	
	SET @startRow = _pageSize * ( _pageIndex - 1 );
	
	SET @pageSize = _pageSize;
	
	SET @rowindex = 0;#行号
#合并字符串
	
	SET @strsql = CONCAT(#'select sql_calc_found_rows  @rowindex:=@rowindex+1 as rownumber,' #记录行号
		'select sql_calc_found_rows ',
		_fields,
		' from ',
		_tables,
		CASE
				IFNULL( _where, '' ) 
				WHEN '' THEN
				'' ELSE CONCAT( ' where ', _where, ' and usrName like concat(\'%\',?,\'%\')' ) 
			END,
		CASE
				IFNULL( _orderby, '' ) 
				WHEN '' THEN
				'' ELSE CONCAT( ' order by ', _orderby ) 
			END,
			' limit ',
			@startRow,
			',',
			@pageSize 
		);
	PREPARE strsql 
	FROM
		@strsql;#定义预处理语句
	EXECUTE strsql USING @p;#执行预处理语句
	DEALLOCATE PREPARE strsql;#删除定义
#通过 sql_calc_found_rows 记录没有使用 limit 语句的记录，使用 found_rows() 获取行数
	
	SET _totalcount = FOUND_ROWS( );#计算总页数
	IF
		( _totalcount <= _pageSize ) THEN
			
			SET _pagecount = 1;
		ELSE 
			SET _pagecount = ( _totalcount + _pageSize - 1 ) / _pageSize;
		
	END IF;#计算求和字段
	IF
		( IFNULL( _sumfields, '' ) <> '' ) THEN#序列sum结果
			
			SET @sumCols = CONCAT ( 'CONCAT_WS(\',\',', 'SUM(', REPLACE ( _sumfields, ',', '),SUM(' ), '))' );#拼接字符串
		
		SET @sumsql = CONCAT(
			'select ',
			@sumCols,
			' INTO @sumResult from ',
			_tables,
			CASE
					IFNULL( _where, '' ) 
					WHEN '' THEN
					'' ELSE CONCAT( ' where ', _where, ' and usrName like concat(\'%\',?,\'%\')' ) 
				END,
				';' 
		);#select @sumsql;
		PREPARE sumsql 
		FROM
			@sumsql;#定义预处理语句
		EXECUTE sumsql USING @p;
		
		SET _sumResult = @sumResult;#执行预处理语句
		DEALLOCATE PREPARE sumsql;#删除定义
		
	END IF;

END
;;
delimiter ;

SET FOREIGN_KEY_CHECKS = 1;
